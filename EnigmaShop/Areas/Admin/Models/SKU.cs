using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EnigmaShop.Areas.Admin.Models
{
    public class SKU
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<SKUOption> SKUOptions { get; set; }


        public SKU()
        {
            SKUOptions = new Collection<SKUOption>();
        }
    }
}
