using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EnigmaShop.Models;

namespace EnigmaShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<SKU> SKUs { get; set; }
        public DbSet<SKUOption> SKUOptions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionGroup> OptionGroups { get; set; }
        public DbSet<SKUPicture> SKUPictures { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //set the OptionId Foreign key in SKUOption cascade delete to false
            builder.Entity<SKUOption>()
                .HasOne(x => x.Option)
                .WithMany(x => x.SKUOptions)
                .OnDelete(DeleteBehavior.Restrict);

            //Set the OptionGroupId Foreign key in SKUOption cascade delete to false
            builder.Entity<SKUOption>()
                .HasOne(x => x.OptionGroup)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            //Set the SKUId Foreign key in SKUOption cascade delete to false
            builder.Entity<SKUOption>()
                .HasOne(x => x.SKU)
                .WithMany(x => x.SKUOptions)
                .OnDelete(DeleteBehavior.Restrict);

            //Set the IsAvaiable bool on SKU table default value to true
            builder.Entity<SKU>()
                .Property(x => x.IsAvailable)
                .HasDefaultValue(true);
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
