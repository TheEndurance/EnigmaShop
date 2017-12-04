using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Models;
using EnigmaShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EnigmaShop.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var shoppingCartItems = await _shoppingCart.GetNumberOfCartItems();
            var shoppingCartTotal = await _shoppingCart.GetShoppingCartTotal();

            var shoppingCartSummaryViewModel = new ShoppingCartSummaryViewModel
            {
                ShoppingCartItems = shoppingCartItems,
                ShoppingCartTotal = shoppingCartTotal
            };

            return View(shoppingCartSummaryViewModel);

        }

    }
}
