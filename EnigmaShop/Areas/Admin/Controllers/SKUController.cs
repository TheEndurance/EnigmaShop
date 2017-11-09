using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnigmaShop.Areas.Admin.Models;
using EnigmaShop.Areas.Admin.ViewModels;
using EnigmaShop.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;

namespace EnigmaShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SKUController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public SKUController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Admin/SKU
        public async Task<IActionResult> Index(int? id)
        {
            IQueryable<SKU> skus = _context.SKUs;

            if (id.HasValue)
            {
                skus = skus.Where(x => x.ProductId == (int) id);
            }

            skus = skus.Include(s => s.Product)
                .Include(x => x.SKUPictures)
                .Include(x => x.SKUOptions)
                .ThenInclude(x => x.OptionGroup);

            ViewData["Header"] = "SKUs";
            return View(await skus.ToListAsync());
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
            ViewData["Header"] = "SKUs";
            return View(sKU);
        }

        // GET: Admin/SKU/Create
        public async Task<IActionResult> Create(int? id)
        {
            var skuFormViewModel = new SKUFormViewModel
            {
                ProductList = new SelectList(await _context.Products.ToListAsync(), "Id", "Name"),
                OptionGroups = await _context.OptionGroups.Include(x => x.Options).ToListAsync()
            };

            ViewData["Header"] = "SKUs";
            return View("SKUForm", skuFormViewModel);
        }

        // POST: Admin/SKU/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,Price,DiscountedPrice,IsAvailable,IsDiscounted,Stock,ImageUrl,OptionIds,Files")] SKUFormViewModel skuFormViewModel)
        {
 
            if (skuFormViewModel.OptionIds.Any(optId => optId == null))
            {
                ModelState.AddModelError("OptionIds", "Option select fields are required");
            }
   

            if (ModelState.IsValid)
            {
                //create new SKU
                var sku = new SKU(skuFormViewModel);

                //Add SKU Options
                await sku.UpdateSKUOptions(skuFormViewModel, _context);

                //Add SKU Pictures
                await sku.UpdateSKUPictures(skuFormViewModel.Files, _environment);

                //Add SKU to DB
                _context.Add(sku);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            skuFormViewModel.ProductList = new SelectList(await _context.Products.ToListAsync(), "Id", "Name");
            skuFormViewModel.OptionGroups = await _context.OptionGroups.Include(x => x.Options).ToListAsync();
            ViewData["Header"] = "SKUs";
            return View("SKUForm", skuFormViewModel);
        }

        // GET: Admin/SKU/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sku = await _context.SKUs.Include(x=>x.SKUPictures).Include(x => x.SKUOptions).ThenInclude(x => x.OptionGroup).SingleOrDefaultAsync(m => m.Id == id);

            if (sku == null)
            {
                return NotFound();
            }

            var skuFormViewModel = new SKUFormViewModel(sku);
            skuFormViewModel.ProductList = new SelectList(await _context.Products.ToListAsync(), "Id", "Name");
            skuFormViewModel.OptionGroups = await _context.OptionGroups.Include(x => x.Options).ToListAsync();
            ViewData["Header"] = "SKUs";
            return View("SKUForm", skuFormViewModel);
        }

        // POST: Admin/SKU/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Price,DiscountedPrice,IsAvailable,IsDiscounted,Stock,ImageUrl,OptionIds,Files")] SKUFormViewModel skuFormViewModel)
        {

            if (id != skuFormViewModel.Id)
            {
                return NotFound();
            }

            var sku = await _context.SKUs.Include(x=>x.SKUPictures).Include(x => x.SKUOptions).ThenInclude(x => x.OptionGroup).SingleOrDefaultAsync(x => x.Id == skuFormViewModel.Id);

            if (sku == null) return NotFound();

            if (skuFormViewModel.OptionIds.Any(optId => optId == null))
            {
                ModelState.AddModelError("OptionIds", "Option select fields are required.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //edit SKU properties
                    sku.EditSKU(skuFormViewModel, _context);
                    
                    //Update sku options
                    await sku.UpdateSKUOptions(skuFormViewModel, _context);

                    //Update sku pictures
                    await sku.UpdateSKUPictures(skuFormViewModel.Files, _environment);

                    //update sku
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
            skuFormViewModel.OptionGroups = await _context.OptionGroups.Include(x => x.Options).ToListAsync();
            ViewData["Header"] = "SKUs";
            return View("SKUForm", skuFormViewModel);
        }


        private bool SKUExists(int id)
        {
            return _context.SKUs.Any(e => e.Id == id);
        }
    }
}
