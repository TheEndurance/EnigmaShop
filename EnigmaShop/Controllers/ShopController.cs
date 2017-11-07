using System;
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

        public async Task<IActionResult> Products(string[] category,string[] option,int? offset)
        {
            int rootCategoryId = _context.Categories.SingleOrDefault(x => x.Name == category[0]).Id;
            if (rootCategoryId == 0) return NotFound();

            var categories = await _context.Categories
                .Include(x => x.Categories)
                .ThenInclude(x => x.Categories)
                .SingleOrDefaultAsync(x => x.RootCategoryId == rootCategoryId && x.ParentCategoryId==null);
                
         

            IQueryable<Product> products = _context.Products
                .Include(x=>x.AltSKUPicture)
                .Include(x=>x.MainSKUPicture)
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category);

            foreach (var cat in category)
            {
                products = products.Where(x => x.ProductCategories.Select(y => y.Category.Name).Contains(cat));
            }


            var shopViewModel = new ShopViewModel();
            shopViewModel.Products = await products.ToListAsync();
            shopViewModel.Categories = categories;
            return View(shopViewModel);
        }
    }
}