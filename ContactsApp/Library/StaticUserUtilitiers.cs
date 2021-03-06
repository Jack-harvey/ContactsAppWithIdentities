using ContactsApp.Areas.Identity.Data;

namespace ContactsApp.Library
{
    public static class StaticUserUtilitiers
    {
        public static string GetUserTheme(ApplicationUser appUser)
        {
            string defaultTheme = "atom-one-light-theme";

            return appUser?.SelectedTheme ?? defaultTheme;

        }
    }
}
