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

        private const decimal TAX_FRACTION = 0.14m;

        public string CartId { get; set; }

        public IList<ShoppingCartItem> ShoppingCartItems { get; set; }

        private ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ShoppingCartItem>> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems =
                       await _context.ShoppingCartItems
                       .Include(x => x.SKU)
                       .ThenInclude(x => x.SKUPictures)
                       .Include(x => x.SKU.Option)
                       .Include(x => x.SKU.Product)
                       .Include(x => x.SKUOption)
                       .ThenInclude(x => x.Size)
                       .Where(x => x.ShoppingCartId == CartId)
                       .ToListAsync());
        }

        public async Task<int> GetNumberOfCartItems()
        {
            return await _context.ShoppingCartItems.CountAsync(x => x.ShoppingCartId == CartId);
        }


        public static ShoppingCart GetCart(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
            string cartId = httpContext?.Request.Cookies[ShoppingCart.ShoppingCartCookieKey] ?? Guid.NewGuid().ToString();

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(7);

            httpContext?.Response.Cookies.Append(ShoppingCart.ShoppingCartCookieKey, cartId, options);

            return new ShoppingCart(context) { CartId = cartId };
        }

        public async Task<bool> AddToCart(SKU sku, SKUOption skuOption)
        {
            var shoppingCartItemFromDb = await
                _context.ShoppingCartItems
                .SingleOrDefaultAsync(x => x.ShoppingCartId == CartId && x.SKUId == sku.Id && x.SKUOptionId == skuOption.Id);

            if (shoppingCartItemFromDb == null)
            {
                if (skuOption.Stock == 0) return false;

                var shoppingCartItem = new ShoppingCartItem
                {
                    SKUId = sku.Id,
                    SKUOptionId = skuOption.Id,
                    Amount = 1,
                    ShoppingCartId = CartId,
                    Date = DateTime.Now
                };
                _context.Add(shoppingCartItem);
            }
            else
            {
                if (shoppingCartItemFromDb.Amount == skuOption.Stock) return false;

                shoppingCartItemFromDb.Amount++;

            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromCart(SKU sku, SKUOption skuOption)
        {
            var shoppingCartItemFromDb =
                await _context.ShoppingCartItems
                .SingleOrDefaultAsync(x => x.ShoppingCartId == CartId && x.SKUId == sku.Id && x.SKUOptionId == skuOption.Id);

            if (shoppingCartItemFromDb == null) return false;

            _context.Remove(shoppingCartItemFromDb);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IncreaseQuantity(SKU sku, SKUOption skuOption)
        {
            var shoppingCartItemFromDb =
                await _context.ShoppingCartItems
                    .SingleOrDefaultAsync(x => x.ShoppingCartId == CartId && x.SKUId == sku.Id && x.SKUOptionId == skuOption.Id);

            if (shoppingCartItemFromDb == null) return false;

            if (shoppingCartItemFromDb.Amount == skuOption.Stock) return false;

            shoppingCartItemFromDb.Amount++;

            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DecreaseQuantity(SKU sku, SKUOption skuOption)
        {
            var shoppingCartItemFromDb =
                await _context.ShoppingCartItems
                    .SingleOrDefaultAsync(x => x.ShoppingCartId == CartId && x.SKUId == sku.Id && x.SKUOptionId == skuOption.Id);

            if (shoppingCartItemFromDb == null) return false;

            if (shoppingCartItemFromDb.Amount > 1)
            {
                shoppingCartItemFromDb.Amount--;
            }
            else
            {
                _context.Remove(shoppingCartItemFromDb);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCart()
        {
            var shoppingCartItemsFromDb =
                await GetShoppingCartItems();

            if (!shoppingCartItemsFromDb.Any()) return false;

            _context.ShoppingCartItems.RemoveRange(shoppingCartItemsFromDb);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetShoppingCartTotal()
        {
            var shoppingCartItemsFromDb =
                await _context.ShoppingCartItems
                .Where(x => x.ShoppingCartId == CartId)
                .Include(x => x.SKU)
                .Include(x => x.SKUOption)
                .ToListAsync();

            decimal total = 0.00m;
            foreach (var item in shoppingCartItemsFromDb)
            {
                total += CalculateSKUTotal(item.SKU, item.Amount);
            }
            return total;
        }

        public decimal CalculateSKUTotal(SKU sku, int amount)
        {
            decimal skuPrice = (sku.IsDiscounted) ? sku.DiscountedPrice : sku.Price;
            return (skuPrice * amount);
        }

        public decimal CalculateTotalTax(decimal shoppingCartTotal)
        {
            return shoppingCartTotal * TAX_FRACTION;
        }
    }
}
