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

        public async Task<IActionResult> Products(string primaryCat, string secondaryCat, int?[] option, int? offset)
        {
            string categoryQueryString = "?";
            string optionQueryString = string.Empty;
            int rootCategoryId = _context.Categories.SingleOrDefault(x => x.Name == primaryCat).Id;
            if (rootCategoryId == 0) return NotFound();

            // Categories
            var categories = await _context.Categories
                .Include(x => x.Categories)
                .ThenInclude(x => x.Categories)
                .SingleOrDefaultAsync(x => x.RootCategoryId == rootCategoryId && x.ParentCategoryId == null);

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
                        .Include(x => x.Product)
                        .Include(x => x.Product.AltSKUPicture)
                        .Include(x => x.Product.MainSKUPicture)
                        .Include(x => x.Option)
                        .Where(x => x.Option.Id==optionId);

                    optionQueryString += $"&option={optionId}";
                }


                //if there are any skus found
                if (skus != null)
                {
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

            //category filtering 

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

            var shopViewModel = new ShopViewModel
            {
                Products = productsList,
                Categories = categories,
                CategoryQueryString = categoryQueryString,
                OptionQueryString = optionQueryString
            };
            return View(shopViewModel);
        }
    }
}