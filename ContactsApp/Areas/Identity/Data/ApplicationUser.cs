using Microsoft.AspNetCore.Identity;

namespace ContactsApp.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? SelectedTheme { get; set; }
    }
}
