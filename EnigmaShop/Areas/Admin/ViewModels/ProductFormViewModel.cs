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

        [DisplayName("Primary Image")]
        public int? MainSKUId { get; set; }

        [DisplayName("Secondary Image")]
        public int? AltSKUId { get; set; }

        public SelectList PrimaryCategoryList{ get; set; }

        public SelectList SecondaryCategoryList { get; set; }

        public SelectList TertiaryCategoryList { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

        public ICollection<SKU> SKUs { get; set; }

        public ProductFormViewModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            ProductCategories = product.ProductCategories;
            SKUs = product.SKUs;
            MainSKUId = product.MainSKUId;
            AltSKUId = product.AltSKUId;

            if (product.ProductCategories == null) return;

            PrimaryCategoryId = product.ProductCategories.SingleOrDefault(x => x.Order == 1)?.CategoryId ?? 0;

            SecondaryCategoryId = product.ProductCategories.SingleOrDefault(x => x.Order==2)?.CategoryId ?? 0;

            TertiaryCategoryId = product.ProductCategories.SingleOrDefault(x => x.Order==3)?.CategoryId;
        }

        public ProductFormViewModel()
        {
            
        }

    }
}
