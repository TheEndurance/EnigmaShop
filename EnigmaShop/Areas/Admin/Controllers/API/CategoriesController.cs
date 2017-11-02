using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Data;
using Microsoft.AspNetCore.Mvc;

namespace EnigmaShop.Areas.Admin.Controllers.API
{
    [Route("admin/api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCategoryByGroupId(int id)
        {
            var categories = _context.Categories.Where(x => x.ParentCategoryId == id).ToList();
            if (!categories.Any())
                return NotFound();

            return Json(categories);
        }


    }
}