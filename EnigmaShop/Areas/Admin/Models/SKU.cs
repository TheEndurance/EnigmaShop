using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.ViewModels;
using EnigmaShop.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task EditSKU(SKUFormViewModel skuFormViewModel,ApplicationDbContext applicationDbContext)
        {
            ProductId = skuFormViewModel.ProductId;
            Price = skuFormViewModel.Price;
            DiscountedPrice = skuFormViewModel.DiscountedPrice;
            IsAvailable = skuFormViewModel.IsAvailable;
            IsDiscounted = skuFormViewModel.IsDiscounted;
            Stock = skuFormViewModel.Stock;
            ImageUrl = skuFormViewModel.ImageUrl;
            await AddSKUOptions(skuFormViewModel, applicationDbContext);

        }

        public async Task AddSKUOptions(SKUFormViewModel skuFormViewModel, ApplicationDbContext applicationDbContext)
        {
            if (skuFormViewModel.OptionIds.Length > 0)
            {
                foreach (var optionId in skuFormViewModel.OptionIds)
                {
                    if (optionId == null) continue;

                    int optId = (int)optionId;

                    var option = await applicationDbContext.Options.Include(x => x.OptionGroup)
                        .SingleOrDefaultAsync(x => x.Id == optId);

                    var optionGroupId = option?.OptionGroupId ?? -1;

                    var skuOpt = SKUOptions.FirstOrDefault(x => x.OptionGroupId== optionGroupId);
                    //check if this sku has this sku option
                    if (skuOpt != null) // this sku has this sku option (so we just need to edit it)
                    {
                        if (optId == -1) // -1 indicates no value selected in the drop down list
                        {
                            SKUOptions.Remove(skuOpt);
                        }
                        else
                        {
                            skuOpt.OptionId = optId;
                        }
                    }
                    else // this sku does not have this sku option (so we must add it)
                    {
                        if (optId == -1) continue; // -1 indicates no value selected in the drop down list

                        if (option == null) continue; //some error occured and this option could not be found

                        AddSKUOption(option);
                    }
                }
            }
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
