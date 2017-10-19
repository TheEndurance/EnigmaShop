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
    public class SKUController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SKUController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/SKU
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SKUs.Include(s => s.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/SKU/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sKU = await _context.SKUs
                .Include(s => s.Product)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sKU == null)
            {
                return NotFound();
            }

            return View(sKU);
        }

        // GET: Admin/SKU/Create
        public async Task<IActionResult> Create()
        {
            var skuFormViewModel = new SKUFormViewModel
            {
                ProductList = new SelectList(await _context.Products.ToListAsync(), "Id", "Name")
            };
            ViewData["Header"] = "SKUs";
            return View(skuFormViewModel);
        }

        // POST: Admin/SKU/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,Price,DiscountedPrice,IsAvailable,IsDiscounted,Stock,ImageUrl")] SKUFormViewModel skuFormViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new SKU(skuFormViewModel));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            skuFormViewModel.ProductList = new SelectList(await _context.Products.ToListAsync(),"Id","Name");
            return View(skuFormViewModel);
        }

        // GET: Admin/SKU/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sKU = await _context.SKUs.SingleOrDefaultAsync(m => m.Id == id);
            if (sKU == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", sKU.ProductId);
            return View(sKU);
        }

        // POST: Admin/SKU/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Price,DiscountedPrice,IsAvailable,IsDiscounted,Stock,ImageUrl")] SKU sKU)
        {
            if (id != sKU.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sKU);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SKUExists(sKU.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", sKU.ProductId);
            return View(sKU);
        }

        // GET: Admin/SKU/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sKU = await _context.SKUs
                .Include(s => s.Product)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sKU == null)
            {
                return NotFound();
            }

            return View(sKU);
        }

        // POST: Admin/SKU/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sKU = await _context.SKUs.SingleOrDefaultAsync(m => m.Id == id);
            _context.SKUs.Remove(sKU);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SKUExists(int id)
        {
            return _context.SKUs.Any(e => e.Id == id);
        }
    }
}
