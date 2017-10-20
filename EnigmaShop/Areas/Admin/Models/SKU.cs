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
    public class SKU
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [Required]
        public decimal Price { get; set; }


        public decimal DiscountedPrice { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public bool IsDiscounted { get; set; }

        [Required]
        public int Stock { get; set; }

        public string ImageUrl { get; set; }

        public HashSet<SKUOption> SKUOptions { get; set; }


        public SKU()
        {
            SKUOptions = new HashSet<SKUOption>();
        }

        public SKU(SKUFormViewModel skuFormViewModel)
        {
            Id = skuFormViewModel.Id;
            ProductId = skuFormViewModel.ProductId;
            Product = skuFormViewModel.Product;
            Price = skuFormViewModel.Price;
            DiscountedPrice = skuFormViewModel.DiscountedPrice;
            IsAvailable = skuFormViewModel.IsAvailable;
            IsDiscounted = skuFormViewModel.IsDiscounted;
            Stock = skuFormViewModel.Stock;
            ImageUrl = skuFormViewModel.ImageUrl;
            SKUOptions = new HashSet<SKUOption>();
        }

        public void EditSKU(SKUFormViewModel skuFormViewModel,int colourOptionId,int sizeOptionId)
        {
            ProductId = skuFormViewModel.ProductId;
            Price = skuFormViewModel.Price;
            DiscountedPrice = skuFormViewModel.DiscountedPrice;
            IsAvailable = skuFormViewModel.IsAvailable;
            IsDiscounted = skuFormViewModel.IsDiscounted;
            Stock = skuFormViewModel.Stock;
            ImageUrl = skuFormViewModel.ImageUrl;
            SKUOptions.Single(x => x.OptionGroup.Name == "Colour").OptionId = colourOptionId;
            SKUOptions.Single(x => x.OptionGroup.Name == "Size").OptionId = sizeOptionId;
        }

        public void AddSKUOption(Option option)
        {
            SKUOptions.Add(new SKUOption
            {
                SKU = this,
                OptionGroup = option.OptionGroup,
                Option = option

            });
        }
    }
}
