using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Controllers;
using EnigmaShop.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
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

        public IEnumerable<Option> OptionList { get; set; }

        public IEnumerable<Size> SizeList { get; set; }

        public int OptionId { get; set; }

        public int[] SizeIds { get; set; }

        public decimal[] Price { get; set; }

        public decimal[] DiscountedPrice { get; set; }

        public bool[] IsAvailable { get; set; }

        public bool[] IsDiscounted { get; set; }

        public int[] Stock { get; set; }

        [DisplayName("Add new SKU Images")]
        public List<IFormFile> Files { get; set; }

        public ICollection<SKUPicture> SKUPictures { get; set; }

        public ICollection<SKUOption> SKUOptions  { get; set; }

        public SKUFormViewModel(SKU sku)
        {
            Id = sku.Id;
            ProductId = sku.ProductId;
            Product = sku.Product;
            SKUPictures = sku.SKUPictures;
            SKUOptions = sku.SKUOptions;
        }

        public SKUFormViewModel()
        {
            SKUPictures = new Collection<SKUPicture>();
            SKUOptions = new Collection<SKUOption>();
        }

        public string Action
        {
            get
            {
                Expression<Func<SKUController, Task<IActionResult>>> create = gc => gc.Create(0);
                Expression<Func<SKUController,Task<IActionResult>>> edit = gc => gc.Edit(null);
                var action = (Id == 0) ? create : edit;
                string methodName = (action.Body as MethodCallExpression)?.Method.Name;
                return methodName;
            }
        }
    }
}
