using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EnigmaShop.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Products(string categoryGroup,int[] categoryId)
        {
            return View();
        }
    }
}