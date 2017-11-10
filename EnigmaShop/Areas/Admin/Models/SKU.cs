using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.ViewModels;
using EnigmaShop.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

        [DisplayName("Discounted Price")]
        public decimal DiscountedPrice { get; set; }


        [Required]
        [DisplayName("Available")]
        public bool IsAvailable { get; set; }

        [DisplayName("Discounted")]
        public bool IsDiscounted { get; set; }

        [Required]
        public int Stock { get; set; }

        public HashSet<SKUOption> SKUOptions { get; set; }

        public ICollection<SKUPicture> SKUPictures { get; set; }

        public SKU()
        {
            SKUOptions = new HashSet<SKUOption>();
            SKUPictures = new Collection<SKUPicture>();
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
            SKUOptions = new HashSet<SKUOption>();
            SKUPictures = new Collection<SKUPicture>();
        }

        public void EditSKU(SKUFormViewModel skuFormViewModel,ApplicationDbContext applicationDbContext)
        {
            ProductId = skuFormViewModel.ProductId;
            Price = skuFormViewModel.Price;
            DiscountedPrice = skuFormViewModel.DiscountedPrice;
            IsAvailable = skuFormViewModel.IsAvailable;
            IsDiscounted = skuFormViewModel.IsDiscounted;
            Stock = skuFormViewModel.Stock;
        }

        public async Task UpdateSKUOptions(SKUFormViewModel skuFormViewModel, ApplicationDbContext applicationDbContext)
        {
            if (skuFormViewModel.OptionIds.Length > 0)
            {
                foreach (var optionId in skuFormViewModel.OptionIds)
                {
                    if (optionId == null) continue; // if no option was selected

                    int optId = (int)optionId;

                    var option = await applicationDbContext.Options.Include(x => x.OptionGroup)
                        .SingleOrDefaultAsync(x => x.Id == optId); // get the option from database using optId

                    var optionGroupId = option?.OptionGroupId ?? -1; //check the option's OptionGroupId

                    var skuOpt = SKUOptions.FirstOrDefault(x => x.OptionGroupId== optionGroupId); //use the OptionGroupId to see if this sku has any SKUOptions with that OptionGroupId

                    //check if this sku has this sku option
                    if (skuOpt != null) // this sku has this sku option (so we just need to edit it)
                    {
                            skuOpt.OptionId = optId;   
                    }
                    else // this sku does not have this sku option (so we must add it)
                    {
                        if (option == null) continue; //some error occured and this option could not be found

                        AddSKUOption(option);
                    }
                }
            }
        }

        public async Task UpdateSKUPictures(IList<IFormFile> files, IHostingEnvironment environment)
        {

            // Add Pictures to server directory and create SKUPictures
            var uploads = Path.Combine(environment.WebRootPath, "uploads");

            if (files?.Count > 0)
            {
                foreach (var formFile in files)
                {
                    if (formFile.Length <= 0) continue;

                    var imageUrl = Path.Combine("/uploads/", formFile.FileName);

                    if (SKUPictures.Any(x => x.ImageUrl == imageUrl)) continue; //if this image is already saved
                    
                    using (var fileStream = new FileStream(Path.Combine(uploads, formFile.FileName), FileMode.Create))
                    {
                        await formFile.CopyToAsync(fileStream);
                    }
                    AddSKUPicture(imageUrl, 100);
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

        public void AddSKUPicture(string imageUrl,int sorting)
        {
            SKUPictures.Add(new SKUPicture
            {
                SKU = this,
                ImageUrl = imageUrl,
                Sorting = sorting
            });
        }
    }
}
