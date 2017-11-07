using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;

namespace EnigmaShop.ViewModels
{
    public class ShopViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Category Categories { get; set; }
    }
}
