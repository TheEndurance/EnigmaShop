using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;

namespace EnigmaShop.Areas.Admin.ViewModels
{
    public class SKUOptionViewModel
    {
        
        public int Id { get; set; }

        public int SKUId { get; set; }

        public Size Size { get; set; }

        public int SizeId { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public decimal DiscountedPrice { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsDiscounted { get; set; }
    }
}
