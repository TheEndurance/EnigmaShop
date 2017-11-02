using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EnigmaShop.Areas.Admin.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        public ICollection<SKU> SKUs { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

        public Product()
        {
            SKUs = new Collection<SKU>();
            ProductCategories = new Collection<ProductCategory>();
        }
        public Product(ProductFormViewModel productFormViewModel)
        {
            ProductCategories = new Collection<ProductCategory>();
            SKUs = new Collection<SKU>();
            Id = productFormViewModel.Id;
            Name = productFormViewModel.Name;
            Description = productFormViewModel.Description;
            AddProductCategories(productFormViewModel);
        }

        public void EditProduct(ProductFormViewModel productFormViewModel)
        {
            Name = productFormViewModel.Name;
            Description = productFormViewModel.Description;
            AddProductCategories(productFormViewModel);
        }

        private void AddProductCategories(ProductFormViewModel productFormViewModel)
        {
            if (ProductCategories.Any(x => x.Order == 1))
            {
                ProductCategories.Single(x => x.Order == 1).CategoryId = productFormViewModel.PrimaryCategoryId;
            }
            else
            {
                ProductCategories.Add(new ProductCategory(this.Id, productFormViewModel.PrimaryCategoryId, 1));
            }

            if (ProductCategories.Any(x => x.Order == 2))
            {
                ProductCategories.Single(x => x.Order == 2).CategoryId = productFormViewModel.SecondaryCategoryId;
            }
            else
            {
                ProductCategories.Add(new ProductCategory(this.Id, productFormViewModel.SecondaryCategoryId, 2));
            }


            if (productFormViewModel.TertiaryCategoryId == null)
            {
                if (ProductCategories.Any(x => x.Order == 3))
                {
                    ProductCategories.Remove(ProductCategories.Single(x => x.Order == 3));
                }
            }
            else
            {
                if (ProductCategories.Any(x => x.Order == 3))
                {
                    ProductCategories.Single(x => x.Order == 3).CategoryId =
                        (int) productFormViewModel.TertiaryCategoryId;
                }
                else
                {
                    ProductCategories.Add(
                        new ProductCategory(this.Id, (int)productFormViewModel.TertiaryCategoryId, 3));
                }
            }

        }
    }
}
