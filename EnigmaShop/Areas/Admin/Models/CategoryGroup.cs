using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnigmaShop.Areas.Admin.Models
{
    public class CategoryGroup
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        public ICollection<Category> Categories { get; set; }

        public CategoryGroup()
        {
            Categories = new Collection<Category>();
        }
    }
}
