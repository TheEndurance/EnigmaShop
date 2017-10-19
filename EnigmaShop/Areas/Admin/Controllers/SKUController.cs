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
                ProductList = new SelectList(await _context.Products.ToListAsync(), "Id", "Name"),
                ColourOptionsList = new SelectList(await _context.Options.Where(x => x.OptionGroup.Name == "Colour").ToListAsync(), "Id", "Name"),
                SizeOptionsList = new SelectList(await _context.Options.Where(x => x.OptionGroup.Name == "Size").ToListAsync(), "Id", "Name"),
                ColourOptionGroupId = (await _context.OptionGroups.SingleOrDefaultAsync(x => x.Name == "Colour")).Id,
                SizeOptionGroupId = (await _context.OptionGroups.SingleOrDefaultAsync(x => x.Name == "Size")).Id

            };
            ViewData["Header"] = "SKUs";
            return View(skuFormViewModel);
        }

        // POST: Admin/SKU/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,Price,DiscountedPrice,IsAvailable,IsDiscounted,Stock,ImageUrl,ColourOptionId,SizeOptionId,ColourOptionGroupId,SizeOptionGroupId")] SKUFormViewModel skuFormViewModel)
        {
            if (ModelState.IsValid)
            {
                var sku = new SKU(skuFormViewModel);

                if (skuFormViewModel.ColourOptionId != 0)
                {
                    sku.AddSKUOption(skuFormViewModel.ColourOptionGroupId, skuFormViewModel.ColourOptionId);

                }
                if (skuFormViewModel.SizeOptionId != 0)
                {
                    sku.AddSKUOption(skuFormViewModel.SizeOptionGroupId, skuFormViewModel.SizeOptionId);
                }
                _context.Add(sku);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            skuFormViewModel.ProductList = 
                new SelectList(await _context.Products.ToListAsync(), "Id", "Name");
            skuFormViewModel.ColourOptionsList =
                new SelectList(await _context.Options.Where(x => x.OptionGroup.Name == "Colour").ToListAsync(), "Id",
                    "Name");
            skuFormViewModel.SizeOptionsList =
                new SelectList(await _context.Options.Where(x => x.OptionGroup.Name == "Size").ToListAsync(), "Id",
                    "Name");
            skuFormViewModel.ColourOptionGroupId =
                (await _context.OptionGroups.SingleOrDefaultAsync(x => x.Name == "Colour")).Id;
            skuFormViewModel.SizeOptionGroupId =
                (await _context.OptionGroups.SingleOrDefaultAsync(x => x.Name == "Size")).Id;

            ViewData["Header"] = "SKUs";
            return View(skuFormViewModel);
        }

        // GET: Admin/SKU/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sku = await _context.SKUs.Include(x => x.SKUOptions).ThenInclude(x => x.OptionGroup).SingleOrDefaultAsync(m => m.Id == id);
            if (sku == null)
            {
                return NotFound();
            }
            var skuFormViewModel = new SKUFormViewModel(sku);
            ViewData["Header"] = "SKUs";
            return View(skuFormViewModel);
        }

        // POST: Admin/SKU/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Price,DiscountedPrice,IsAvailable,IsDiscounted,Stock,ImageUrl,ColourOptionId,SizeOptionId")] SKUFormViewModel skuFormViewModel)
        {
            if (id != skuFormViewModel.Id)
            {
                return NotFound();
            }

            var sku = _context.SKUs.SingleOrDefault(x => x.Id == skuFormViewModel.Id);
            if (sku == null) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    sku.EditSKU(skuFormViewModel);
                    _context.Update(sku);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SKUExists(skuFormViewModel.Id))
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
            skuFormViewModel.ProductList = new SelectList(await _context.Products.ToListAsync(), "Id", "Name");
            ViewData["Header"] = "SKUs";
            return View(skuFormViewModel);
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
