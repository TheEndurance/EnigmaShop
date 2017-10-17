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
    public class Product
    {
        public int Id { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public ICollection<SKU> SKUs { get; set; }

        public Product()
        {
            SKUs = new Collection<SKU>();
        }
    }
}
