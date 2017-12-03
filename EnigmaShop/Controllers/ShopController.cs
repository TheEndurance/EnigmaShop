using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;
using EnigmaShop.Data;
using EnigmaShop.Utilities;
using EnigmaShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace EnigmaShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Products(string primaryCat, string secondaryCat, int?[] options, int?[] sizes, int page = 1, int perPage = 2)
        {
            page = (page > 0) ? page : 1;
            perPage = (perPage > 50) ? 50 : perPage;

            // INITIALIZE AND SET : Categories
            IQueryable<Category> categories = _context.Categories.Where(x => x.ParentCategoryId == null);

            if (!string.IsNullOrWhiteSpace(primaryCat))
            {
                categories = categories.Where(x => x.Name == primaryCat);
            }

            categories = categories.Include(x => x.Categories).ThenInclude(x => x.Categories);

            var categoryList = await categories.ToListAsync();


            // INITIALIZE : product and sku queries 
            IQueryable<SKU> skus = _context.SKUs
                .Include(x => x.SKUOptions)
                .Include(x=>x.SKUPictures)
                .Include(x => x.Product)
                .ThenInclude(x=>x.ProductCategories)
                .Include(x => x.Option);

            //FILTER : SKU by options if there are any
            if (options.Any(x => x.HasValue))
            {
                int[] optionIds = options.Cast<int>().ToArray();

                //find all skus with this option id
                skus = skus.Where(x => optionIds.Contains(x.OptionId));

            }

            //FILTER : SKU by sizes if there are any
            if (sizes.Any(x => x.HasValue))
            {
                int[] sizeIds = sizes.Cast<int>().ToArray();

                skus = skus.Where(x => x.SKUOptions.Select(y => y.SizeId).Any(f => sizeIds.Contains(f)));

            }

            //FILTER : Product by category
            if (!string.IsNullOrWhiteSpace(primaryCat))
            {
                var primaryCategory = await _context.Categories.SingleOrDefaultAsync(x => x.Name == primaryCat);
                skus = skus.Where(x => x.Product.ProductCategories.Select(y => y.CategoryId).Contains(primaryCategory.Id));
            }
            

            if (!string.IsNullOrWhiteSpace(secondaryCat))
            {
                var secondaryCategory = await _context.Categories.SingleOrDefaultAsync(x => x.Name == secondaryCat);
                skus = skus.Where(x =>
                    x.Product.ProductCategories.Select(y => y.CategoryId).Contains(secondaryCategory.Id));
            }

            //create skuList
            var skuList = await skus
                .OrderBy(x=>x.Product.Name)
                .Take(page*perPage)   
                .ToListAsync();

            //project into SKU Shop View Models
            var skuShopList = skuList.Select(x => new SKUShopViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Product = x.Product,
                MainSKUPicture = x.SKUPictures.OrderBy(y => y.Sorting).Take(1).SingleOrDefault()?.ImageUrl,
                AltSKUPicture = x.SKUPictures.OrderBy(y => y.Sorting).Skip(1).Take(1).SingleOrDefault()?.ImageUrl,
                Price = x.Price
            });


            // INITIALIZE AND SET : Option groups and options
            //Get the product option groups and options

            var optionGroupList = await _context.OptionGroups
                .Include(x => x.Options)
                .Select(o => new OptionGroup
                {
                    Name = o.Name,
                    Options = o.Options.OrderBy(x => x.Name)
                })
                .ToListAsync();

            //INITIALIZE AND SET : Size groups and sizes
            //Get the product Size groups and sizes

            var sizeGroupList = await _context.SizeGroups
                .Include(x => x.Sizes)
                .Select(s => new SizeGroup
                {
                    Name = s.Name,
                    Sizes = s.Sizes.OrderBy(x => x.Name)
                })
                .ToListAsync();

            var shopViewModel = new ShopViewModel
            {
                SKUs = skuShopList,
                Categories = categoryList,
                OptionGroups = optionGroupList,
                SizeGroups = sizeGroupList,
                PrimaryCategory = primaryCat,
                SecondaryCategory = secondaryCat,
                Page = page,
                PerPage = perPage,
                OptionIds = options.Cast<int>().ToArray(),
                SizeIds = sizes.Cast<int>().ToArray(),
                PrimaryCategoryParamName = nameof(primaryCat),
                SecondaryCategoryParamName = nameof(secondaryCat),
                OptionParamName = nameof(options),
                SizeParamName = nameof(sizes),
                PageParamName = nameof(page),
                PerPageParamName = nameof(perPage)
            };
            return View(shopViewModel);
        }

        public async Task<IActionResult> Product(int skuId)
        {
            var sku = await _context.SKUs
                .Include(x=>x.Option)
                .Include(x=>x.Product)
                .Include(x=>x.SKUOptions)
                .ThenInclude(x=>x.Size)
                .Include(x=>x.SKUPictures)
                .SingleOrDefaultAsync(x => x.Id == skuId);

            if (sku == null) return RedirectToAction("Products");

            var relatedSKUs = await _context.SKUs
                .Include(x=>x.Option)
                .Include(x=>x.SKUPictures)
                .Where(x => x.ProductId == sku.ProductId && x.Id != sku.Id)
                .Select(x=>new RelatedSKUsViewModel
                {
                    SKUId = x.Id,
                    OptionName = x.Option.Name,
                    ImageUrl = x.SKUPictures.FirstOrDefault().ImageUrl
                })
                .ToListAsync();

            var optionGroup = await _context.OptionGroups.SingleOrDefaultAsync(x => x.Id == sku.Option.OptionGroupId);


            //find first available skuOption index
            int firstAvailableSKUOptionIndex = -1;

            for (int i=0;i<sku.SKUOptions.Count;i++)
            {
                if (sku.SKUOptions[i].IsAvailable && sku.SKUOptions[i].Stock > 0)
                {
                    firstAvailableSKUOptionIndex = i;
                    break;
                }
            }


            var skuDetailViewModel = new SKUDetailViewModel
            {
                Id = sku.Id,
                Price = sku.Price,
                IsDiscounted = sku.IsDiscounted,
                DiscountedPrice = sku.DiscountedPrice,
                Name = sku.Product.Name,
                Description = sku.Product.Description,
                OptionName = sku.Option.Name,
                SKUOptions = sku.SKUOptions,
                FirstSKUPicture = sku.SKUPictures.FirstOrDefault(),
                SKUPictures = sku.SKUPictures.Skip(1).ToList(),
                FirstAvailableSKUOptionIndex = firstAvailableSKUOptionIndex,
                RelatedSKUs = relatedSKUs,
                OptionGroup = optionGroup

            };

           

            return View(skuDetailViewModel);


        }

      
    }
}