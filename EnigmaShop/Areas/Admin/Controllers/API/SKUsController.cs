using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnigmaShop.Areas.Admin.Controllers.API
{
    [Route("/admin/api/[controller]")]
    public class SKUsController : Controller
    {

        private readonly ApplicationDbContext _context;
        public SKUsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("{id:int}")]
        [HttpDelete]
        public  async Task<IActionResult> DeleteSKUById(int id)
        {
            var sku = await _context.SKUs.SingleOrDefaultAsync(x => x.Id == id);
            if (sku == null)
                return NotFound(id);

            _context.SKUs.Remove(sku);
            await _context.SaveChangesAsync();
            return Ok(id);
        }
    }
}