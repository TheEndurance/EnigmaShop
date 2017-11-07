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
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //set the OptionId Foreign key in SKUOption cascade delete to false
            builder.Entity<SKUOption>()
                .HasOne(x => x.Option)
                .WithMany()
                .HasForeignKey(x => x.OptionId)
                .OnDelete(DeleteBehavior.Restrict);

            //Set the OptionGroupId Foreign key in SKUOption cascade delete to false
            builder.Entity<SKUOption>()
                .HasOne(x => x.OptionGroup)
                .WithMany()
                .HasForeignKey(x => x.OptionGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            //When I delete SKUOption from my SKU, delete the SKUOption row
            builder.Entity<SKUOption>()
                .HasOne(x => x.SKU)
                .WithMany(x => x.SKUOptions)
                .HasForeignKey(x=>x.SKUId)
                .OnDelete(DeleteBehavior.Cascade);

            //Set the IsAvaiable bool on SKU table default value to true
            builder.Entity<SKU>()
                .Property(x => x.IsAvailable)
                .HasDefaultValue(true);
       

            //Set the DiscountedPrice on SKU Table default value to 0.00m

            builder.Entity<SKU>()
                .Property(x => x.DiscountedPrice)
                .HasDefaultValue(0.00m);


            // **
            // PRODUCT CATEGORY
            // **
            builder.Entity<Product>()
                .HasMany(x => x.ProductCategories)
                .WithOne(x => x.Product)
                .OnDelete(DeleteBehavior.Restrict);

            //*** Entity refers to an object of the product class**//
            // When I delete product category from my product entity, I want the product category row in DB to be deleted
            // Foreign key setup

            builder.Entity<ProductCategory>()
                .HasOne(x => x.Product)
                .WithMany(x=>x.ProductCategories)
                .HasForeignKey(x=>x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Foreign key setup
            builder.Entity<ProductCategory>()
                .HasOne(x => x.Category)
                .WithMany(x => x.ProductCategories)
                .HasForeignKey(x => x.CategoryId);

            // CATEGORY
       
            //Set the parent FK for category
            builder.Entity<Category>()
                .HasMany(x => x.Categories)
                .WithOne(x => x.ParentCategory)
                .HasForeignKey(x => x.ParentCategoryId);



            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
