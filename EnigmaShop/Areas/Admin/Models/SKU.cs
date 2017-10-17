using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EnigmaShop.Areas.Admin.Models
{
    public class SKU
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal DiscountedPrice { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public bool IsDiscounted { get; set; }

        [Required]
        public int Stock { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<SKUOption> SKUOptions { get; set; }


        public SKU()
        {
            SKUOptions = new Collection<SKUOption>();
        }
    }
}
