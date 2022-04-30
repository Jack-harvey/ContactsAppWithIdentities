using ContactsApp.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsApp.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Contacts = new HashSet<Contact>();

        }
        [PersonalData]
        public string? SelectedTheme { get; set; }
        [PersonalData]
        public int? ThemeId { get; set; }

        [ForeignKey("ThemeId")]
        [InverseProperty("AspNetUsers")]
        public virtual Theme? Theme { get; set; }

        [InverseProperty("AspNetUser")]
        public virtual ICollection<Contact> Contacts { get; set; }
        //public virtual ICollection<>
    }
}
