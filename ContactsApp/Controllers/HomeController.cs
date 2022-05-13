using ContactsApp.Areas.Identity.Data;
using ContactsApp.Data;
using ContactsApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace ContactsApp.Controllers
{
    [ServiceFilter(typeof(Library.UserThemeFilterService))]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ContactsAppDataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ContactsAppDataContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        //public JsonResult UsernameAndTheme(int userId)
        //{
        //    var users = _context.Users;
        //    var userIdInDb = users.Where(s => s.UserId.Equals(userId));
        //    return Json(userIdInDb);
        //}

        public async Task<JsonResult> SaveNewThemePreference(string themePreference)
        {

            if ((User?.Identity?.IsAuthenticated ?? false) == false || string.IsNullOrEmpty(themePreference))
            {
                return Json(new
                {
                    result = themePreference == "" ? 0 : 1,
                    theme = themePreference,
                    icon = themePreference == "dracula-theme" ?
                    "fa-sun" : "fa-moon",
                    message = themePreference == "" ?
                    "Invalid theme selected" : "Log-in to save theme choice"
                });
            }

            var currentUserObject = await _userManager.GetUserAsync(User);
            if (themePreference == "dracula-theme")
            {
                currentUserObject.ThemeId = 1;
            }
            if (themePreference == "atom-one-dark-theme")
            {
                currentUserObject.ThemeId = 2;
            }
            if (themePreference == "atom-one-light-theme")
            {
                currentUserObject.ThemeId = 3;
            }
            //currentUserObject.SelectedTheme = themePreference;
            await _userManager.UpdateAsync(currentUserObject);
            return Json(new
            {
                result = 1,
                theme = themePreference,
                icon = themePreference == "dracula-theme" ?
                    "fa-sun" : "fa-moon",
                message = themePreference == "dracula-theme" ?
                "It's dracula baby!" : "Click change theme again, you did it wrong."

            });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}