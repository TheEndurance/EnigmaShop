using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Controllers;
using EnigmaShop.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
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

        public IEnumerable<OptionGroup> OptionGroups { get; set; }

        
        public int?[] OptionIds { get; set; }

        public HashSet<SKUOption> SKUOptions { get; set; }


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
            SKUOptions = sku.SKUOptions;
        }

        public SKUFormViewModel()
        {
            SKUOptions = new HashSet<SKUOption>();
        }

        public string Action
        {
            get
            {
                Expression<Func<SKUController, Task<IActionResult>>> create = gc => gc.Create(null);
                Expression<Func<SKUController,Task<IActionResult>>> edit = gc => gc.Edit(null);
                var action = (Id == 0) ? create : edit;
                string methodName = (action.Body as MethodCallExpression)?.Method.Name;
                return methodName;
            }
        }
    }
}
