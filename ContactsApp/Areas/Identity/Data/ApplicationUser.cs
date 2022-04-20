using Microsoft.AspNetCore.Identity;

namespace ContactsApp.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? ThemeSelected { get; set; }
    }
}
