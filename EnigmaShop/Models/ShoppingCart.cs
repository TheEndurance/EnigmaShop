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

        public async void AddToCart(SKU sku)
        {
            var shoppingCartItemFromDb = await
                _context.ShoppingCartItems.SingleOrDefaultAsync(x => x.ShoppingCartId == CartId && x.SKUId == sku.Id);

            if (shoppingCartItemFromDb == null)
            {
                var shoppingCartItem = new ShoppingCartItem
                {
                    SKU = sku,
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

    }
}
