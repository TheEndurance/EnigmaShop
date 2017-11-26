using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;
using EnigmaShop.Data;
using EnigmaShop.QueryConstraint;
using EnigmaShop.Utilities;
using EnigmaShop.ViewModels;
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
                .Include(x => x.SKUPictures)
                .Include(x => x.SKUOptions)
                .Include(x => x.Option)
                .Include(x => x.Product)
                .ThenInclude(x => x.ProductCategories);

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
            var primaryCategory = await _context.Categories.SingleOrDefaultAsync(x => x.Name == primaryCat);
            skus = skus.Where(x => x.Product.ProductCategories.Select(y => y.CategoryId).Contains(primaryCategory.Id));

            if (secondaryCat != null)
            {
                var secondaryCategory = await _context.Categories.SingleOrDefaultAsync(x => x.Name == secondaryCat);
                skus = skus.Where(x =>
                    x.Product.ProductCategories.Select(y => y.CategoryId).Contains(secondaryCategory.Id));
            }

            var skuList = await skus
                .OrderBy(x => x.Product.Name)
                .Skip((page-1)*perPage)
                .Take(perPage)
                .ToListAsync();


            if (!skuList.Any())
            {
                return NotFound(new
                {
                    error = "There are no more products."
                });

            }

            var skuShopList = skuList.Select(x => new SKUShopViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Product = x.Product,
                MainSKUPicture = x.SKUPictures.OrderBy(y => y.Sorting).Take(1).SingleOrDefault()?.ImageUrl,
                AltSKUPicture = x.SKUPictures.OrderBy(y => y.Sorting).Skip(1).Take(1).SingleOrDefault()?.ImageUrl,
                Price = x.SKUOptions.Take(1).SingleOrDefault()?.Price ?? 0.00m
            });


            return new JsonResult(new
            {
                products = skuShopList,
                nextPage = page + 1
            });

        }
    }
}