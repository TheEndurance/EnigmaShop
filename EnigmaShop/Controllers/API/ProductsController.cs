using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;
using EnigmaShop.Data;
using EnigmaShop.QueryConstraint;
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
        public async Task<IActionResult> Products([FromQuery]string primaryCat, [FromQuery]string secondaryCat, [FromQuery]int?[] options,[FromQuery] int?[] sizes, [RequiredQuery]int page, [RequiredQuery]int perPage)
        {
            if (page <= 0) return BadRequest("Page can not be equal 0 or less");
            if (perPage > 50) return BadRequest("perPage can not exceed 50");
            if (perPage <= 0) return BadRequest("perPage can not equal 0 or less");

            // INITIALIZE : product and sku queries 
            IQueryable<Product> products = null;
            IEnumerable<Product> productsList = new List<Product>();
            IQueryable<SKU> skus = _context.SKUs
                .Include(x => x.SKUOptions)
                .Include(x => x.Product)
                .Include(x => x.Product.AltSKUPicture)
                .Include(x => x.Product.MainSKUPicture)
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

            // CONVERT : Skus to Products
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
                    ProductCategories = x.Product.ProductCategories,
                    SizeGroupId = x.Product.SizeGroupId,
                    OptionGroupId = x.Product.OptionGroupId
                });
            }

            // INITIALIZE : Product if null
            if (products == null)
            {
                products = _context.Products
                    .Include(x => x.MainSKUPicture)
                    .Include(x => x.AltSKUPicture);
            }

            //FILTER : Product by category
            products = products
                .Where(x => x.ProductCategories.Select(y => y.Category.Name).Contains(primaryCat));

            if (secondaryCat != null)
            {
                products = products.Where(x =>
                    x.ProductCategories.Select(y => y.Category.Name).Contains(secondaryCat));
            }

            // TO LIST : Product Query tolist
            productsList = await products
                .OrderBy(x => x.Name)
                .Skip((page-1) * perPage)
                .Take(page * perPage)
                .ToListAsync();

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
                nextPage = page+1
            });

        }
    }
}