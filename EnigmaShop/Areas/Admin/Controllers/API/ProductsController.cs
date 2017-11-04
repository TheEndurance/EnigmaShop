using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace EnigmaShop.Areas.Admin.Controllers.API
{
    [Route("admin/api/[controller]")]
    public class ProductsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            var product =  await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return NotFound(id);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(id);
        }
    }
}