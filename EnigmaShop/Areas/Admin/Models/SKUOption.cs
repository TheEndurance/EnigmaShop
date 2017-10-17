using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EnigmaShop.Areas.Admin.Models
{
    public class SKUOption
    {
        public int Id { get; set; }

        public int SKUId { get; set; }

        [ForeignKey(nameof(SKUId))]
        public SKU SKU { get; set; }

        public int OptionId { get; set; }

        [ForeignKey(nameof(OptionId))]
        public Option Option { get; set; }

        public int OptionGroupId { get; set; }

        [ForeignKey(nameof(OptionGroupId))]
        public OptionGroup OptionGroup { get; set; }
        

    }
}
