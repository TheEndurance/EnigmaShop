using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Data;
using Microsoft.AspNetCore.Mvc;

namespace EnigmaShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult AddToCart()
        {
            return View();
        }
    }
}