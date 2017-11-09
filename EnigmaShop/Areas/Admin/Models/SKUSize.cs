using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnigmaShop.Areas.Admin.Models
{
    public class SKUSize
    {
        public int Id { get; set; }

        public int SKUId { get; set; }

        public SKU SKU { get; set; }

        public Size Size { get; set; }

        public int SizeId { get; set; }

        public int Stock { get; set; }

    }
}
