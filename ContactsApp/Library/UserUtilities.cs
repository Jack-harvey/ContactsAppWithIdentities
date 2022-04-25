using ContactsApp.Areas.Identity.Data;

namespace ContactsApp.Library
{
    public class UserUtilities : IUserUtilities
    {
        public string GetUserTheme(ApplicationUser appUser)
        {
            string defaultTheme = "atom-one-dark-theme";

            return appUser?.SelectedTheme ?? defaultTheme;
            //(User?.Identity?.IsAuthenticated ?? false) == false ? ViewData["userTheme"] = defaultTheme : ViewData["userTheme"] = userObject.SelectedTheme;

            //if (!User.Identity?.IsAuthenticated ?? false)
            //if(appUser == null)
            //{
            //    return defaultTheme;
            //}
            //else
            //{
            //    return appUser.SelectedTheme ?? defaultTheme;
            //}
        }
    }
}
