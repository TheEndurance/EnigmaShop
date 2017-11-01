using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EnigmaShop.Areas.Admin.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int CategoryId { get; set; }
        
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int ProductId { get; set; }

        public int Order { get; set; }
    }
}
