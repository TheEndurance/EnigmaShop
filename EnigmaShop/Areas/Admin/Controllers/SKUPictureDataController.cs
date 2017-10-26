using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EnigmaShop.Areas.Admin.Controllers
{
    [Route("admin/api/[controller]")]
    public class SKUPictureDataController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SKUPictureDataController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        [HttpDelete("DeleteById/{id:int}")]
        public async Task<IActionResult> DeleteSKUPictureById(int id)
        {
            var skuPicture = _context.SKUPictures.SingleOrDefault(x => x.Id == id);
            if (skuPicture == null) return NotFound(id);

            _context.SKUPictures.Remove(skuPicture);
            await _context.SaveChangesAsync();
            return Ok(id);
        }
    }
}