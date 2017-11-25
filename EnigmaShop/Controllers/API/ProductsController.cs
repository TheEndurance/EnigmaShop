using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;
using EnigmaShop.Data;
using EnigmaShop.QueryConstraint;
using EnigmaShop.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EnigmaShop.Controllers.API
{

    [Route("/api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Products([FromQuery]string primaryCat, [FromQuery]string secondaryCat, [FromQuery]int?[] options, [FromQuery] int?[] sizes, [RequiredQuery]int page, [RequiredQuery]int perPage)
        {
            if (page <= 0) return BadRequest("Page can not be equal 0 or less");
            if (perPage > 50) return BadRequest("perPage can not exceed 50");
            if (perPage <= 0) return BadRequest("perPage can not equal 0 or less");

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

            var skusList = await skus.ToListAsync();

            // CONVERT : Skus to Products
            if (skusList.Any())
            {
                //convert the skus into product
                productList = skusList.Select(x => new Product
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
                .Skip((page - 1) * perPage)
                .Take(page * perPage);


            if (!productsList.Any())
            {
                return NotFound(new
                {
                    error = "There are no more products."
                });

            }

            return new JsonResult(new
            {
                products = productsList,
                nextPage = page + 1
            });

        }
    }
}