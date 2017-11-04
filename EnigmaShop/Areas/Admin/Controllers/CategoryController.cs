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
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
            ViewData["Header"] = "Category";
            var categories = await _context.Categories
                .Where(x => x.ParentCategoryId == null)
                .Include(x => x.Categories)
                .ThenInclude(x => x.Categories)
                .ToListAsync();
            return View(categories);

        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            var categoryFormViewModel = new CategoryFormViewModel();
            ViewData["Header"] = "Category";
            return View("CategoryForm",categoryFormViewModel);
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Order,ParentCategoryId,RootCategoryId")] CategoryFormViewModel categoryFormViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category(categoryFormViewModel);
                category.Order = 100;

                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Header"] = "Category";
            return View("CategoryForm",categoryFormViewModel);
        }

        public IActionResult CreateSubCategory(int parentId, int rootId)
        {
            var categoryFormViewModel = new CategoryFormViewModel();
            categoryFormViewModel.ParentCategoryId = parentId;
            categoryFormViewModel.RootCategoryId = rootId;
            ViewData["Header"] = "Category";
            return View("CategoryForm",categoryFormViewModel);
        }


        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryFormViewModel = new CategoryFormViewModel(category);
            return View("CategoryForm", categoryFormViewModel);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Order,ParentCategoryId,RootCategoryId")] CategoryFormViewModel categoryFormViewModel)
        {
            if (id != categoryFormViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var category = _context.Categories.Single(x => x.Id == categoryFormViewModel.Id);
                    category.Name = categoryFormViewModel.Name;

                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(categoryFormViewModel.Id))
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
            return View("CategoryForm",categoryFormViewModel);
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
