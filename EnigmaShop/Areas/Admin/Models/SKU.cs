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

        [ForeignKey(nameof(OptionId))]
        public Option Option { get; set; }

        public int OptionId { get; set; }

        public ICollection<SKUPicture> SKUPictures { get; set; }

        public IList<SKUOption> SKUOptions { get; set; }

        public SKU()
        {
            SKUPictures = new Collection<SKUPicture>();
            SKUOptions = new List<SKUOption>();
        }

        public SKU(SKUFormViewModel skuFormViewModel)
        {
            Id = skuFormViewModel.SKUId;
            ProductId = skuFormViewModel.ProductId;
            Product = skuFormViewModel.Product;
            OptionId = skuFormViewModel.OptionId;
            SKUPictures = new Collection<SKUPicture>();
            SKUOptions = new List<SKUOption>();
        }

        public void EditSKU(SKUFormViewModel skuFormViewModel)
        {
            OptionId = skuFormViewModel.OptionId;
        }


        public async Task UpdateSKUOptions(SKUFormViewModel skuFormViewModel, ApplicationDbContext context)
        {
            //A list of skuOptions that are null from the skuFormViewModel which can then be deleted
            //var skuOptionsToDelete = SKUOptions
            //    .Select(skuOption => new {skuOption = skuFormViewModel.SKUOptions.SingleOrDefault(i => i.Id == skuOption.Id)})
            //    .Where(item => item.skuOption == null)
            //    .Select(item => item.skuOption)
            //    .ToList();

            //if (skuOptionsToDelete.Any())
            //{
            //    foreach (var skuOpt in skuOptionsToDelete)
            //    {
            //        context.Entry(skuOpt).State = EntityState.Deleted;
            //    }
            //}

            foreach (var skuOption in skuFormViewModel.SKUOptions)
            {
                if (skuOption.Id <= 0) continue;

                var skuOptionInDb = SKUOptions.Single(x => x.Id == skuOption.Id);
                context.Entry(skuOptionInDb).CurrentValues.SetValues(skuOption);
                context.Entry(skuOptionInDb).State = EntityState.Modified;
            }
            await context.SaveChangesAsync();
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

        private void AddSKUPicture(string imageUrl,int sorting)
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
