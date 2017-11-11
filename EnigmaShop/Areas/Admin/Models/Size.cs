using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EnigmaShop.Areas.Admin.Models
{
    public class Size
    {
        public int Id { get; set; }

        [ForeignKey(nameof(SizeGroupId))]
        public SizeGroup SizeGroup { get; set; }

        public int SizeGroupId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }


    }
}
