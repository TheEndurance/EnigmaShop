using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnigmaShop.Areas.Admin.ViewModels
{
    public class SKUFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public decimal Price { get; set; }
        
        [DisplayName("Discounted Price")]
        public decimal DiscountedPrice { get; set; }

        [Required]
        [DisplayName("Is this SKU Available?")]
        public bool IsAvailable { get; set; }

        [DisplayName("Is this SKU Discounted?")]
        public bool IsDiscounted { get; set; }

        [Required]
        public int Stock { get; set; }

        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }

        public SelectList ProductList { get; set; }

        public SKUFormViewModel(SKU sku)
        {
            Id = sku.Id;
            ProductId = sku.ProductId;
            Product = sku.Product;
            Price = sku.Price;
            DiscountedPrice = sku.DiscountedPrice;
            IsAvailable = sku.IsAvailable;
            IsDiscounted = sku.IsDiscounted;
            Stock = sku.Stock;
            ImageUrl = sku.ImageUrl;
        }

        public SKUFormViewModel()
        {
            
        }
    }
}
