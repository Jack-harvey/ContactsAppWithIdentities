using ContactsApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ContactsApp.Library
{
    public class UserThemeFilterService : IResultFilter
    {

        private readonly Data.ContactsAppDataContext _data;
        private readonly Data.ApplicationDbContext _userData;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly ClaimsPrincipal _user;
        public UserThemeFilterService(Data.ContactsAppDataContext data, Data.ApplicationDbContext userData, UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _data = data;
            _userData = userData;
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
            return appUser?.SelectedTheme ?? defaultTheme;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var controller = context.Controller as Controller;
            string themeName = ThemeNameFromDbForLayout().GetAwaiter().GetResult();
            if (controller != null)
                controller.ViewData["userTheme"] = themeName;
            ///base.OnResultExecuting(context);
        }
    }
}
