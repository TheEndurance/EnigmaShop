﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EnigmaShop.Areas.Admin.Models
{
    public class SizeGroup
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public IEnumerable<Size> Sizes { get; set; }

        public SizeGroup()
        {
            Sizes = new Collection<Size>();
        }
        
    }
}
