using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;

namespace EnigmaShop.ViewModels
{
    public class ShopViewModel
    {
        //Models
        public List<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<OptionGroup> OptionGroups { get; set; }
        public IEnumerable<SizeGroup> SizeGroups { get; set; }

        //params
        public string PrimaryCategory { get; set; }
        public string SecondaryCategory { get; set; }
        public int[] OptionIds { get; set; }
        public int[] SizeIds { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }

        //param names
        public string OptionParamName { get; set; }
        public string SizeParamName { get; set; }
        public string PrimaryCategoryParamName { get; set; }
        public string SecondaryCategoryParamName { get; set; }
        public string PageParamName { get; set; }
        public string PerPageParamName { get; set; }
        
        
    }
}
