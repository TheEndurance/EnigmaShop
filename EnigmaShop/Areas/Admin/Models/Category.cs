using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EnigmaShop.Areas.Admin.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int Order { get; set; }

        public int? ParentCategoryId { get; set; }

        public int RootCategoryId { get; set; }

        public Category ParentCategory { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

        public Category(CategoryFormViewModel categoryFormViewModel)
        {
            Id = categoryFormViewModel.Id;
            Name = categoryFormViewModel.Name;
            Order = categoryFormViewModel.Order;
            ParentCategoryId = categoryFormViewModel.ParentCategoryId;
            RootCategoryId = categoryFormViewModel.RootCategoryId;
            Categories = new Collection<Category>();
            ProductCategories = new Collection<ProductCategory>();
        }

        public Category()
        {
            Categories = new Collection<Category>();
            ProductCategories = new Collection<ProductCategory>();
        }

    }
}
