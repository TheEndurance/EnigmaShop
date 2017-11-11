using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EnigmaShop.Areas.Admin.Models
{
    public class SKUSize
    {
        public int Id { get; set; }

        [ForeignKey(nameof(SKUId))]
        public SKU SKU { get; set; }

        public int SKUId { get; set; }

        [ForeignKey(nameof(SizeId))]
        public Size Size { get; set; }

        public int SizeId { get; set; }

        public int Stock { get; set; }

    }
}
