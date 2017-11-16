using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;

namespace EnigmaShop.ViewModels
{
    public class ShopViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<OptionGroup> OptionGroups { get; set; }
        public IEnumerable<SizeGroup> SizeGroups { get; set; }
        public string CategoryQueryString { get; set; }
        public string OptionQueryString { get; set; }
        public string PrimaryCategory { get; set; }
        public string SecondaryCategory { get; set; }
        
    }
}
