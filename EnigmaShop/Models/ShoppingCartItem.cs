using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;

namespace EnigmaShop.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public string ShoppingCartId { get; set; }

        public int SKUId { get; set; }

        [ForeignKey(nameof(SKUId))]
        public SKU SKU{ get; set; }

        public int SKUOptionId { get; set; }

        [ForeignKey(nameof(SKUOptionId))]
        public SKUOption SKUOption { get; set; }

        public int Amount { get; set; }

        public DateTime Date { get; set; }


    }
}
