using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;
using EnigmaShop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnigmaShop.Models
{
    public class ShoppingCart
    {
        private readonly ApplicationDbContext _context;

        public static readonly string ShoppingCartCookieKey = "ShoppingCart";

        public string CartId { get; set; }

        public IList<ShoppingCartItem> ShoppingCartItems { get; set; }

        private ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ShoppingCartItem>> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems =
                       await _context.ShoppingCartItems.Where(x => x.ShoppingCartId == CartId).ToListAsync());
        }


        public static ShoppingCart GetCart(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
            string cartId = httpContext?.Request.Cookies[ShoppingCart.ShoppingCartCookieKey] ?? Guid.NewGuid().ToString();

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(7);

            httpContext?.Response.Cookies.Append(ShoppingCart.ShoppingCartCookieKey, cartId);

            return new ShoppingCart(context) {CartId = cartId};
        }

        public async void AddToCart(SKU sku,SKUOption skuOption)
        {
            var shoppingCartItemFromDb = await
                _context.ShoppingCartItems
                .SingleOrDefaultAsync(x => x.ShoppingCartId == CartId && x.SKUId == sku.Id && x.SKUOptionId == skuOption.Id);

            if (shoppingCartItemFromDb == null)
            {
                var shoppingCartItem = new ShoppingCartItem
                {
                    SKU = sku,
                    SKUOption = skuOption,
                    Amount = 1,
                    ShoppingCartId = CartId,
                    Date = DateTime.Now
                };
                _context.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItemFromDb.Amount++;
            }

            await _context.SaveChangesAsync();
        }

        public async void RemoveFromCart(SKU sku,SKUOption skuOption)
        {
            var shoppingCartItemFromDb =
                await _context.ShoppingCartItems
                .SingleOrDefaultAsync(x => x.ShoppingCartId == CartId && x.SKUId == sku.Id && x.SKUOptionId == skuOption.Id);

            if (shoppingCartItemFromDb == null) return;

            if (shoppingCartItemFromDb.Amount > 1)
            {
                shoppingCartItemFromDb.Amount--;
            }
            else
            {
                _context.Remove(shoppingCartItemFromDb);
            }
            await _context.SaveChangesAsync();
        }

        public async void ClearCart()
        {
            var shoppingCartItemsFromDb =
                await GetShoppingCartItems();

            if (!shoppingCartItemsFromDb.Any()) return;

            _context.ShoppingCartItems.RemoveRange(shoppingCartItemsFromDb);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetShoppingCartTotal()
        {
            var shoppingCartItemsFromDb =
                await _context.ShoppingCartItems
                .Where(x => x.ShoppingCartId == CartId)
                .Include(x=>x.SKU)
                .Include(x=>x.SKUOption)
                .ToListAsync();
            
            

            decimal total = 0.00m;
            return total;
        }

    }
}
