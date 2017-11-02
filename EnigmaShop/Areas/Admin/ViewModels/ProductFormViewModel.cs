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

        [DisplayName("Primary Category")]
        public int PrimaryCategoryId { get; set; }

        [DisplayName("Secondary Category")]
        public int SecondaryCategoryId { get; set; }

        [DisplayName("Tertiary Category")]
        public int? TertiaryCategoryId { get; set; }


        public SelectList PrimaryCategoryList{ get; set; }

        public ProductFormViewModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            if (product.ProductCategories != null)
                PrimaryCategoryId = product.ProductCategories.SingleOrDefault(x => x.Category.ParentCategoryId == null)?.CategoryId ?? 0;

        }

        public ProductFormViewModel()
        {
            
        }

    }
}
