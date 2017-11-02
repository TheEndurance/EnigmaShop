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
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public Product Product { get; set; }

        public int Order { get; set; }


        public ProductCategory(int productId, int categoryId, int order)
        {
            ProductId = productId;
            CategoryId = categoryId;
            Order = order;
        }

        public ProductCategory()
        {
            
        }
    }
}
