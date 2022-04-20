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

            //int fakeUserId = 2;
            //var userIdInDb = _data.Users.Find(fakeUserId);
            string defaultTheme = "atom-one-dark-theme";
            if (!_user.Identity?.IsAuthenticated ?? false)
            {
                return defaultTheme;
            }

            var appUser = await _userManager.GetUserAsync(_user);
            
                
                
           return appUser?.SelectedTheme ?? defaultTheme;


            //var loggedInUser = User;
            
            //if (userIdInDb == null)
            //    return "";
            //if (userIdInDb.ThemeSelection == null)
            //    return "";
            //if (userIdInDb.ThemeSelection == "Dracula")
            //    return "dracula-theme";
            //if (userIdInDb.ThemeSelection == "atomOneDark")
            //    return "atom-one-dark-theme";

            //return userIdInDb.ThemeSelection.ToString();
        }







        public void OnResultExecuted(ResultExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var controller = context.Controller as Controller;
            string themeName = ThemeNameFromDbForLayout().GetAwaiter().GetResult();
            if(controller != null)
                controller.ViewData["userTheme"] = themeName;
            ///base.OnResultExecuting(context);
        }
    }
}
