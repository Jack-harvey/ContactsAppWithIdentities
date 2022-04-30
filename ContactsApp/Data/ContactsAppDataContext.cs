using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ContactsApp.Models;
using ContactsApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace ContactsApp.Data
{
    public partial class ContactsAppDataContext : IdentityDbContext<ApplicationUser>
    {
        public ContactsAppDataContext()
        {
        }

        public ContactsAppDataContext(DbContextOptions<ContactsAppDataContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<CompanyOffice> CompanyOffices { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Theme> Themes { get; set; } = null!;
        //public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<VwCompanyCount> VwCompanyCounts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=localhost;Database=ContactsAppData;Trusted_Connection=True;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
            //    entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.ThemeId)
                    .HasConstraintName("FK_AspNetUsers_Themes");

                //entity.HasMany(d => d.Roles)
                //    .WithMany(p => p.Users)
                //    .UsingEntity<Dictionary<string, object>>(
                //        "AspNetUserRole",
                //        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                //        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                //        j =>
                //        {
                //            j.HasKey("UserId", "RoleId");

                //            j.ToTable("AspNetUserRoles");

                //            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                //        });
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyId).ValueGeneratedNever();
            });

            modelBuilder.Entity<CompanyOffice>(entity =>
            {
                entity.Property(e => e.OfficeId).ValueGeneratedNever();

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyOffices)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyOffices_Companies");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.ContactId).ValueGeneratedNever();

                entity.HasOne(d => d.AspNetUser)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.AspNetUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contacts_AspNetUsers");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contacts_Categories");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Contacts_Companies");
            });

            modelBuilder.Entity<VwCompanyCount>(entity =>
            {
                entity.ToView("vwCompanyCount");
            });

            base.OnModelCreating(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
