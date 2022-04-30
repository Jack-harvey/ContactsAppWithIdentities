using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ContactsApp.Areas.Identity.Data;

namespace ContactsApp.Models
{
    public partial class Theme
    {
        public Theme()
        {
            AspNetUsers = new HashSet<ApplicationUser>();
        }

        [Key]
        public int ThemeId { get; set; }
        [StringLength(255)]
        public string? ClassName { get; set; }
        [StringLength(255)]
        public string? FriendlyName { get; set; }
        [StringLength(512)]
        public string? ToastDescription { get; set; }

        [InverseProperty("Theme")]
        public virtual ICollection<ApplicationUser> AspNetUsers { get; set; }
    }
}
