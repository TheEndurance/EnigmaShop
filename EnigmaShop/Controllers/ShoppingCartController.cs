using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Data;
using EnigmaShop.Models;
using EnigmaShop.ViewModels;
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

        public async Task<IActionResult> Index()
        {
            var shoppingCartItems = await _shoppingCart.GetShoppingCartItems();
            var shoppingCartViewModel = shoppingCartItems.Select(x => new ShoppingCartViewModel
            {
                ShoppingCartItem = x,
                SKUPicture = x.SKU.SKUPictures.FirstOrDefault(),
                ItemSubTotal = _shoppingCart.CalculateSKUTotal(x.SKU,x.Amount)
            }).ToList();
            ViewData["Title"] = "Shopping Cart Summary";
            return View(shoppingCartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int SKUId,int SKUOptionId)
        {

            var sku = await _context.SKUs.SingleOrDefaultAsync(x => x.Id == SKUId);

            if (sku == null)
            {
                TempData["Message"] = "Invalid SKU can not be added to cart";
                return RedirectToAction("Products", "Shop");
            }

            var skuOption =
                await _context.SKUOptions.SingleOrDefaultAsync(x => x.Id == SKUOptionId && x.SKUId == SKUId);
            if (skuOption == null)
            {
                TempData["Message"] = "Invalid or no size was selected, try again";
                return RedirectToAction("Product","Shop",new {skuId=SKUId});
            }

            await _shoppingCart.AddToCart(sku, skuOption);

            return RedirectToAction("Product", "Shop", new {skuId = SKUId});
        }


        public async Task<IActionResult> RemoveItemFromCart(int SKUId, int SKUOptionId)
        {
            var sku = await _context.SKUs.SingleOrDefaultAsync(x => x.Id == SKUId);

            if (sku == null)
            {
                TempData["Message"] = "Invalid SKU can not be added to cart";
                return RedirectToAction("Products", "Shop");
            }

            var skuOption =
                await _context.SKUOptions.SingleOrDefaultAsync(x => x.Id == SKUOptionId && x.SKUId == SKUId);
            if (skuOption == null)
            {
                TempData["Message"] = "Invalid or no size was selected, try again";
                return RedirectToAction("Product", "Shop", new { skuId = SKUId });
            }

            await _shoppingCart.RemoveFromCart(sku, skuOption);

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> DecreaseItemQuantity(int SKUId, int SKUOptionId)
        {
            var sku = await _context.SKUs.SingleOrDefaultAsync(x => x.Id == SKUId);

            if (sku == null)
            {
                TempData["Message"] = "Invalid SKU can not be added to cart";
                return RedirectToAction("Products", "Shop");
            }

            var skuOption =
                await _context.SKUOptions.SingleOrDefaultAsync(x => x.Id == SKUOptionId && x.SKUId == SKUId);
            if (skuOption == null)
            {
                TempData["Message"] = "Invalid or no size was selected, try again";
                return RedirectToAction("Product", "Shop", new { skuId = SKUId });
            }

            await _shoppingCart.DecreaseQuantity(sku, skuOption);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> IncreaseItemQuantity(int SKUId, int SKUOptionId)
        {
            var sku = await _context.SKUs.SingleOrDefaultAsync(x => x.Id == SKUId);

            if (sku == null)
            {
                TempData["Message"] = "Invalid SKU can not be added to cart";
                return RedirectToAction("Products", "Shop");
            }

            var skuOption =
                await _context.SKUOptions.SingleOrDefaultAsync(x => x.Id == SKUOptionId && x.SKUId == SKUId);
            if (skuOption == null)
            {
                TempData["Message"] = "Invalid or no size was selected, try again";
                return RedirectToAction("Product", "Shop", new { skuId = SKUId });
            }

            await _shoppingCart.IncreaseQuantity(sku, skuOption);

            return RedirectToAction("Index");
        }

        public async  Task<IActionResult> ClearCartItems()
        {
            await _shoppingCart.ClearCart();

            return RedirectToAction("Index");
        }
    }
}