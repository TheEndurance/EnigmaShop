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

        public async Task<IActionResult> Products(string primaryCat, string secondaryCat, int?[] options, int?[] sizes, int page = 1, int perPage = 10)
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
                .Include(x => x.Product)
                .Include(x=>x.Product.ProductCategories)
                .Include(x => x.Product.AltSKUPicture)
                .Include(x => x.Product.MainSKUPicture)
                .Include(x => x.Option);
            IEnumerable<Product> productList = null;

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

            var skuList = await skus.ToListAsync();

            // CONVERT : Skus to Products
            if (skuList.Any())
            {
                //convert the skus into product
                productList = skuList.Select(x => new Product
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                    Description = x.Product.Description,
                    Price = x.Product.Price,
                    AltSKUId = x.Product.AltSKUId,
                    MainSKUId = x.Product.MainSKUId,
                    MainSKUPicture = x.Product.MainSKUPicture,
                    AltSKUPicture = x.Product.AltSKUPicture,
                    ProductCategories = x.Product.ProductCategories,
                    SizeGroupId = x.Product.SizeGroupId,
                    OptionGroupId = x.Product.OptionGroupId
                });
            }

            // INITIALIZE : Product if null
            if (productList == null)
            {
                productList = await _context.Products
                    .Include(x => x.MainSKUPicture)
                    .Include(x => x.AltSKUPicture)
                    .ToListAsync();
            }



            //FILTER : Product by category
            var primaryCatId = await _context.Categories.SingleOrDefaultAsync(x => x.Name == primaryCat);
            productList = productList
                .Where(x => x.ProductCategories.Select(y => y.CategoryId).Contains(primaryCatId.Id));

            if (secondaryCat != null)
            {
                var secondaryCatId = await _context.Categories.SingleOrDefaultAsync(x => x.Name == secondaryCat);
                productList = productList.Where(x =>
                    x.ProductCategories.Select(y => y.CategoryId).Contains(secondaryCatId.Id));
            }

            // TO LIST : Product Query tolist
            var productsList = productList
                .OrderBy(x => x.Name)
                .Distinct(new ProductComparer())
                .Take(page * perPage);

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
                Products = productsList,
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

      
    }
}