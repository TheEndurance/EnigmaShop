using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;

namespace EnigmaShop.ViewModels
{
    public class SKUDetailViewModel
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string OptionName { get; set; }

        public IList<SKUOption> SKUOptions { get; set; }

        public int FirstAvailableSKUOptionIndex { get; set; }

        public IEnumerable<Size> Sizes { get; set; }

        public SKUPicture FirstSKUPicture { get; set; }

        public IEnumerable<SKUPicture> SKUPictures { get; set; }

        public IEnumerable<RelatedSKUsViewModel> RelatedSKUs { get; set; }

        public OptionGroup OptionGroup { get; set; }
       
    }
}
