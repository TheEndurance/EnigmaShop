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
                skus = skus.Where(x => x.ProductId == (int)id);
            }

            //TODO include Sku size
            skus = skus
                .Include(x => x.Option)
                .Include(x => x.SKUPictures)
                .Include(s => s.Product)
                .ThenInclude(x => x.OptionGroup)
                .Include(x => x.SKUOptions)
                .ThenInclude(x => x.Size);

            var skuList = await skus.ToListAsync();

            foreach (var sku in skuList)
            {
                sku.SKUPictures = new List<SKUPicture>(sku.SKUPictures.OrderBy(x => x.Sorting));
            }


            ViewData["Header"] = "SKUs";
            ViewBag.ProductList = new SelectList(await _context.Products.ToListAsync(), "Id", "Name");
            return View(skuList);
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
        public async Task<IActionResult> Create(int id)

        {
            var product = await _context.Products
                .Include(x => x.SizeGroup)
                .ThenInclude(x => x.Sizes)
                .SingleOrDefaultAsync(x => x.Id == (int)id);

            var skuFormViewModel = new SKUFormViewModel
            {
                Product = product,
                ProductId = product.Id,
                OptionList = await _context.Options.Where(x => x.OptionGroupId == product.OptionGroupId).ToListAsync(),
                SizeList = await _context.Sizes.Where(x => x.SizeGroupId == product.SizeGroupId).ToListAsync(),

        };
            foreach (var size in product.SizeGroup.Sizes)
            {
                skuFormViewModel.SKUOptions.Add(new SKUOption
                {
                    Size = size,
                    SizeId = size.Id
                });
            }

            ViewData["Header"] = "SKUs";
            return View("SKUForm", skuFormViewModel);
        }

        // POST: Admin/SKU/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SKUId,ProductId,ImageUrl,OptionId,Files,SKUOptions")] SKUFormViewModel skuFormViewModel)
        {

            //TODO Validate SKU Size has been entered


            if (ModelState.IsValid)
            {
                //create new SKU
                var sku = new SKU(skuFormViewModel);

                //Add SKU Options
                sku.SKUOptions = skuFormViewModel.SKUOptions;

                //Add SKU Pictures
                await sku.UpdateSKUPictures(skuFormViewModel.Files, _environment);

                //Add SKU to DB
                _context.Add(sku);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var product = await _context.Products.Include(x => x.SizeGroup)
                .ThenInclude(x => x.Sizes)
                .SingleOrDefaultAsync(x => x.Id == skuFormViewModel.ProductId);
            skuFormViewModel.Product = product;
            skuFormViewModel.OptionList =
                await _context.Options.Where(x => x.OptionGroupId == product.OptionGroupId).ToListAsync();
            skuFormViewModel.SizeList = await _context.Sizes.Where(x => x.SizeGroupId == product.SizeGroupId).ToListAsync();
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

            var sku = await _context.SKUs
                .Include(x=>x.Product)
                .Include(x=>x.SKUOptions)
                .ThenInclude(x=>x.Size)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (sku == null)
            {
                return NotFound();
            }

            var skuFormViewModel = new SKUFormViewModel(sku);
            skuFormViewModel.SKUPictures = await _context.SKUPictures.Where(x => x.SKUId == sku.Id)
                .OrderBy(x => x.Sorting).ToListAsync();
            skuFormViewModel.OptionList =
                await _context.Options.Where(x => x.OptionGroupId == sku.Product.OptionGroupId).ToListAsync();
            ViewData["Header"] = "SKUs";
            return View("SKUForm", skuFormViewModel);
        }

        // POST: Admin/SKU/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SKUId,ImageUrl,SKUOptions,OptionId,Files")] SKUFormViewModel skuFormViewModel)
        {

            if (id != skuFormViewModel.SKUId)
            {
                return NotFound();
            }

            //TODO Include SKUOption

            var sku = await _context.SKUs
                .Include(x=>x.SKUOptions)
                .Include(x => x.SKUPictures)
                .SingleOrDefaultAsync(x => x.Id == skuFormViewModel.SKUId);

            if (sku == null) return NotFound();

            //TODO validate sku sizes

            if (ModelState.IsValid)
            {
                try
                {
                    //edit SKU properties
                    sku.EditSKU(skuFormViewModel);

                    ////Update sku options
                    await sku.UpdateSKUOptions(skuFormViewModel, _context);

                    //Update sku pictures
                    await sku.UpdateSKUPictures(skuFormViewModel.Files, _environment);

                    //update sku
                    _context.Update(sku);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SKUExists(skuFormViewModel.SKUId))
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

            skuFormViewModel = new SKUFormViewModel(sku)
            {
                SKUPictures = await _context.SKUPictures.Where(x=>x.SKUId==sku.Id).OrderBy(x=>x.Sorting).ToListAsync(),
                OptionList =
                    await _context.Options.Where(x => x.OptionGroupId == sku.Product.OptionGroupId).ToListAsync(),
                Product = await _context.Products.SingleOrDefaultAsync(x => x.Id == sku.Product.Id)
            };

            ViewData["Header"] = "SKUs";
            return View("SKUForm", skuFormViewModel);
        }


        private bool SKUExists(int id)
        {
            return _context.SKUs.Any(e => e.Id == id);
        }
    }
}
