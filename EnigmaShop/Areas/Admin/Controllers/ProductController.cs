using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnigmaShop.Areas.Admin.Models;
using EnigmaShop.Areas.Admin.ViewModels;
using EnigmaShop.Data;

namespace EnigmaShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;

        }

        // GET: Admin/Product
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category);
            ViewData["Header"] = "Products";
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(x=>x.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["Header"] = "Products";
            return View(product);
        }

        // GET: Admin/Product/Create
        public async Task<IActionResult> Create()
        {
            var productForm = new ProductFormViewModel
            {
                PrimaryCategoryList = new SelectList(await _context.Categories.Where(x=>x.ParentCategoryId==null).ToListAsync(),"Id","Name")
            };
            ViewData["Header"] = "Products";

            return View(productForm);
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Name,PrimaryCategoryId,SecondaryCategoryId,TertiaryCategoryId")] ProductFormViewModel productFormViewModel)
        {
            if (ModelState.IsValid)
            {
                var product = new Product(productFormViewModel);
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            productFormViewModel.PrimaryCategoryList =
                new SelectList(await _context.Categories.Where(x=>x.ParentCategoryId==null).ToListAsync(), "Id", "Name");
            ViewData["Header"] = "Products";
            return View(productFormViewModel);
        }

        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(x=>x.ProductCategories).SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var productFormViewModel = new ProductFormViewModel(product);

            productFormViewModel.PrimaryCategoryList =
                new SelectList(await _context.Categories.Where(x => x.ParentCategoryId == null).ToListAsync(), "Id", "Name");
            productFormViewModel.SecondaryCategoryList = new SelectList(
                await _context.Categories.Where(x => x.ParentCategoryId == productFormViewModel.PrimaryCategoryId).ToListAsync(),
                "Id", "Name");
            productFormViewModel.TertiaryCategoryList = new SelectList(
                await _context.Categories.Where(x => x.ParentCategoryId == productFormViewModel.SecondaryCategoryId)
                    .ToListAsync(), "Id", "Name");

            //TODO:
            //productFormViewModel.CategoryList = new SelectList(await _context.Categories
            //    .Where(x => x.CategoryGroupId == product.CategoryGroupId)
            //    .ToListAsync(),"Id","Type",product.CategoryId);
            ViewData["Header"] = "Products";
            return View(productFormViewModel);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Name,PrimaryCategoryId,SecondaryCategoryId,TertiaryCategoryId")] ProductFormViewModel productFormViewModel)
        {
            if (id != productFormViewModel.Id)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(x=>x.ProductCategories).SingleOrDefaultAsync(x => x.Id == productFormViewModel.Id);
            if (product == null) return NotFound();
               
            if (ModelState.IsValid)
            {
                try
                {
                    product.EditProduct(productFormViewModel);
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(productFormViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            productFormViewModel.PrimaryCategoryList =
                new SelectList(await _context.Categories.Where(x => x.ParentCategoryId == null).ToListAsync(), "Id", "Name");
            ViewData["Header"] = "Products";
            return View(productFormViewModel);
        }

     

        // GET: Admin/Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCategories)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["Header"] = "Products";
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
