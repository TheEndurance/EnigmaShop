using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async  Task<IActionResult> GetCategoryByGroupId(int id)
        {
            var categories = await _context.Categories.Where(x => x.ParentCategoryId == id).ToListAsync();
            if (!categories.Any())
                return NotFound();

            return Json(categories);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategoryById(int id)
        {
            var category = await _context.Categories.Include(x=>x.Categories).ThenInclude(x=>x.Categories).SingleOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound(id);
            foreach (var secondaryCategory in category.Categories)
            {
                foreach (var tertiraryCategory in secondaryCategory.Categories)
                {
                    _context.Categories.Remove(tertiraryCategory);
                }
                _context.Categories.Remove(secondaryCategory);
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return Ok(id);
        }


    }
}