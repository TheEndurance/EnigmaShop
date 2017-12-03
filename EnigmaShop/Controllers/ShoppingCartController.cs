using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Data;
using EnigmaShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnigmaShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(ApplicationDbContext context,ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int SKUId,int SKUOptionId)
        {

            var sku = await _context.SKUs.SingleOrDefaultAsync(x => x.Id == SKUId);
            if (sku == null) return Content("Error: Invalid SKU can not be added to cart");

            var skuOption =
                await _context.SKUOptions.SingleOrDefaultAsync(x => x.Id == SKUOptionId && x.SKUId == SKUId);
            if (skuOption == null) return Content("Error: Invalid  can not be added to cart");

            _shoppingCart.AddToCart(sku, skuOption);

            return RedirectToAction("Product", "Shop", new {skuId = SKUId});
        }
    }
}