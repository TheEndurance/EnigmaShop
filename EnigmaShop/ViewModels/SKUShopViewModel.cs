using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;

namespace EnigmaShop.ViewModels
{
    public class SKUShopViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string MainSKUPicture { get; set; }

        public string AltSKUPicture { get; set; }

        public decimal Price { get; set; }

        public string Url { get; set; }
    }
}
