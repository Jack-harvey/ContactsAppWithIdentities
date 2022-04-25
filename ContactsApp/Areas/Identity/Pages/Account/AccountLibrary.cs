using ContactsApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactsApp.Areas.Identity.Pages.Account
{
    public class AccountLibrary : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountLibrary(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async void ThemeGet()
        {

            var userObject = await _userManager.GetUserAsync(User);
            string defaultTheme = "atom-one-dark-theme";

            //ViewData["userTheme"] = !User.Identity?.IsAuthenticated ?? false ? defaultTheme : userObject.SelectedTheme;

            if (!User.Identity?.IsAuthenticated ?? false)
            {
                ViewData["userTheme"] = defaultTheme;
            }
            else
            {
                ViewData["userTheme"] = userObject.SelectedTheme;
            }
        }
    }
}
