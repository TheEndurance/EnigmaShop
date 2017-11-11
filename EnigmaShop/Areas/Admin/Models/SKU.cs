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

        [ForeignKey(nameof(OptionId))]
        public Option Option { get; set; }

        public int OptionId { get; set; }

        public ICollection<SKUPicture> SKUPictures { get; set; }
        public ICollection<SKUSize> SKUSizes { get; set; }

        public SKU()
        {
            SKUPictures = new Collection<SKUPicture>();
            SKUSizes = new Collection<SKUSize>();
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
            SKUPictures = new Collection<SKUPicture>();
            SKUSizes = new Collection<SKUSize>();
        }

        public void EditSKU(SKUFormViewModel skuFormViewModel,ApplicationDbContext applicationDbContext)
        {
            ProductId = skuFormViewModel.ProductId;
            Price = skuFormViewModel.Price;
            DiscountedPrice = skuFormViewModel.DiscountedPrice;
            IsAvailable = skuFormViewModel.IsAvailable;
            IsDiscounted = skuFormViewModel.IsDiscounted;
        }


        public async Task UpdateSKUSizes()
        {
            
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

        public void AddSKUSize(int sizeId, int stock)
        {
            SKUSizes.Add(new SKUSize
            {
                SKU = this,
                SizeId = sizeId,
                Stock = stock
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
