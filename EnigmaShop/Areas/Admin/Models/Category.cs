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
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int Order { get; set; }

        public int? ParentCategoryId { get; set; }

        [ForeignKey("ParentCategoryId")]
        public Category ParentCategory { get; set; }

        public int RootCategoryId { get; set; }

        [ForeignKey("RootCategoryId")]
        public Category RootCategory { get; set; }

        public ICollection<Category> Categories { get; set; }

        public Category()
        {
            Categories = new Collection<Category>();
        }

    }
}
