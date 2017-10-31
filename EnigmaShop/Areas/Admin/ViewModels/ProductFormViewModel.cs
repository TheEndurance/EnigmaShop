using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnigmaShop.Areas.Admin.ViewModels
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        [DisplayName("Category Type")]
        public int CategoryId { get; set; }

        [DisplayName("Category Group Type")]
        public int CategoryGroupId { get; set; }

        public SelectList CategoryGroupList { get; set; }

        public SelectList CategoryList { get; set; }

        public ProductFormViewModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            CategoryId = product.CategoryId;
            CategoryGroupId = product.CategoryGroupId;
        }

        public ProductFormViewModel()
        {
            
        }

    }
}
