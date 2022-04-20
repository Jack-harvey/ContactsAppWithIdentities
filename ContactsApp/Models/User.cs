﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Models
{
    public partial class User
    {
        [Key]
        [Column("userId")]
        public int UserId { get; set; }
        [Column("userName")]
        [StringLength(255)]
        public string UserName { get; set; } = null!;
        [Column("themeSelection")]
        [StringLength(255)]
        public string? ThemeSelection { get; set; }
    }
}
