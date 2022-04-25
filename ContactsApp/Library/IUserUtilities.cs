using ContactsApp.Areas.Identity.Data;

namespace ContactsApp.Library
{
    public interface IUserUtilities
    {
        public string GetUserTheme(ApplicationUser appUser);
    }
}
