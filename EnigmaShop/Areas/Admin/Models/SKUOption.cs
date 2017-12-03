using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EnigmaShop.Areas.Admin.Models
{
    public class SKUOption
    {
        public int Id { get; set; }

        [BindNever]
        [ForeignKey(nameof(SKUId))]
        public SKU SKU { get; set; }

        public int SKUId { get; set; }

        [BindNever]
        [ForeignKey(nameof(SizeId))]
        public Size Size { get; set; }

        public int SizeId { get; set; }

        public int Stock { get; set; }

        public bool IsAvailable { get; set; }




    }
}
