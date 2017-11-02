using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;

namespace EnigmaShop.Areas.Admin.ViewModels
{
    public class CategoryFormViewModel
    {

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int Order { get; set; }

        public int? ParentCategoryId { get; set; }

        public int RootCategoryId { get; set; }

        public ICollection<Category> Categories { get; set; }

        public CategoryFormViewModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            Order = category.Order;
            ParentCategoryId = category.ParentCategoryId;
            RootCategoryId = category.RootCategoryId;
            Categories = category.Categories;
        }

        public CategoryFormViewModel()
        {
            Categories = new Collection<Category>();
        }
    }
}
