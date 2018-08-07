using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WineTime.Models;

namespace WineTime.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<WineProducts> WineProducts { get; set; }
        public DbSet<WineCategory> WineCategories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // builder.Entity<WineCategory>().HasKey("Name");
            // TOP works ok but more likely to typo the property name or pick one that doesn't exist
            // OR -- a better option with the arrow function; catches errors at compile time
            builder.Entity<WineCategory>().HasKey(x => x.Name);

            builder.Entity<WineCategory>().Property(x => x.DateCreated).HasDefaultValueSql("GetDate()");
            builder.Entity<WineCategory>().Property(x => x.DateLastModified).HasDefaultValueSql("GetDate()");
            builder.Entity<WineCategory>().Property(x => x.Name).HasMaxLength(100);
        }
    }
}