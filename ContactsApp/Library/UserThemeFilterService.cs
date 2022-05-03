using ContactsApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ContactsApp.Library
{
    public class UserThemeFilterService : IResultFilter
    {

        private readonly Data.ContactsAppDataContext _data;
        //private readonly Data.ApplicationDbContext _userData;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly ClaimsPrincipal _user;
        public UserThemeFilterService(Data.ContactsAppDataContext data, UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _data = data;
            //_userData = userData;
            _userManager = userManager;
            _user = contextAccessor.HttpContext.User;

            //_contextAccessor = contextAccessor;
        }

        public async Task<string> ThemeNameFromDbForLayout()
        {
            string defaultTheme = "atom-one-light-theme";
            if (!_user.Identity?.IsAuthenticated ?? false)
            {
                return defaultTheme;
            }
            var appUser = await _userManager.GetUserAsync(_user);
            var x = await _data.Users.Include(i => i.Theme).FirstOrDefaultAsync(f => f.Id == _user.FindFirstValue(ClaimTypes.NameIdentifier));
            //return appUser.Theme.ClassName;
            int? themeId = appUser.ThemeId;
            var themeObject = await _data.Themes.FindAsync(themeId);
            return themeObject.ClassName;
            //return appUser?.SelectedTheme ?? defaultTheme;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var controller = context.Controller as Controller;
            var page = context.Controller as PageModel;
            string themeName = ThemeNameFromDbForLayout().GetAwaiter().GetResult();
            bool themeIsLight = themeName == "atom-one-light-theme" ? true : false;

            if (controller != null)
            {
                controller.ViewData["userTheme"] = themeName;
                controller.ViewData["themeIcon"] = themeIsLight == true ? "fa-moon" : "fa-sun";
            }
            if (page != null)
            {
                page.ViewData["userTheme"] = themeName;
                page.ViewData["themeIcon"] = themeIsLight == true ? "fa-moon" : "fa-sun";
            }
            ///base.OnResultExecuting(context);
        }
    }
}
