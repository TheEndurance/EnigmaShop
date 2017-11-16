using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;
using EnigmaShop.Data;
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

        public async Task<IActionResult> Products(string primaryCat, string secondaryCat, int?[] option, int?[] size, int? offset)
        {
            string categoryQueryString = "?";
            string optionQueryString = string.Empty;

            // Categories
            IQueryable<Category> categories = _context.Categories.Where(x=>x.ParentCategoryId==null);

            if (!string.IsNullOrWhiteSpace(primaryCat))
            {
                categories = categories.Where(x=> x.Name == primaryCat);
            }

            categories = categories.Include(x => x.Categories).ThenInclude(x => x.Categories);

            var categoryList = await categories.ToListAsync();

            // Products
            IQueryable<Product> products = null;
            IEnumerable<Product> productsList = new List<Product>();

            //Filter by options if there are any
            if (option.Any(x => x.HasValue))
            {
                int[] optionIds = option.Cast<int>().ToArray();
                IQueryable<SKU> skus = null;
                //find all skus with this option id
                foreach (int optionId in optionIds)
                {
                    skus = _context.SKUs
                        .Where(x => x.Option.Id == optionId);

                    optionQueryString += $"&option={optionId}";
                }

                //if there are any skus found
                if (skus != null)
                {
                    skus = skus.Include(x => x.Product)
                 .Include(x => x.Product.AltSKUPicture)
                 .Include(x => x.Product.MainSKUPicture)
                 .Include(x => x.Option);
                    //convert the skus into product
                    products = skus.Select(x => new Product
                    {
                        Id = x.Product.Id,
                        Name = x.Product.Name,
                        Description = x.Product.Description,
                        AltSKUId = x.Product.AltSKUId,
                        MainSKUId = x.Product.MainSKUId,
                        MainSKUPicture = x.Product.MainSKUPicture,
                        AltSKUPicture = x.Product.AltSKUPicture,
                        ProductCategories = x.Product.ProductCategories
                    });
                }

            }

            //if products haven't been intialized through option filtering
            if (products == null)
            {
                products = _context.Products
                    .Include(x => x.MainSKUPicture)
                    .Include(x => x.AltSKUPicture);
            }

            //category filtering for products

            products = products
                .Where(x => x.ProductCategories.Select(y => y.Category.Name).Contains(primaryCat));
            categoryQueryString += $"primaryCat={primaryCat}";

            if (secondaryCat != null)
            {
                products = products.Where(x =>
                    x.ProductCategories.Select(y => y.Category.Name).Contains(secondaryCat));
                categoryQueryString += $"&secondaryCat={secondaryCat}";
            }

            //create product list from product query
            productsList = await products.ToListAsync();

            //Get the product option groups and options
            var optionGroupIds = productsList.Select(x => x.OptionGroupId).ToHashSet();

            var optionGroupList = await _context.OptionGroups
                .Where(x => optionGroupIds.Contains(x.Id))
                .Include(x => x.Options)
                .ToListAsync();
            

            //Get the product Size groups and sizes
            var sizeGroupIds = productsList.Select(x => x.SizeGroupId).ToHashSet();

            var sizeGroupList = await _context.SizeGroups
                .Where(x => sizeGroupIds.Contains(x.Id))
                .Include(x => x.Sizes)
                .ToListAsync();



            var shopViewModel = new ShopViewModel
            {
                Products = productsList,
                Categories = categoryList,
                OptionGroups = optionGroupList,
                SizeGroups = sizeGroupList,
                CategoryQueryString = categoryQueryString,
                OptionQueryString = optionQueryString,
                PrimaryCategory = primaryCat,
                SecondaryCategory = secondaryCat
            };
            return View(shopViewModel);
        }
    }
}