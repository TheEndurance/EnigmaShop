using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EnigmaShop.Areas.Admin.Models
{
    public class SKUPicture
    {
        public int Id { get; set; }

        public int SKUId { get; set; }

        [ForeignKey(nameof(SKUId))]
        public SKU SKU { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
