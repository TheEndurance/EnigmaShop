using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Areas.Admin.Models;
using EnigmaShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnigmaShop.Areas.Admin.Controllers.API
{
    [Route("admin/api/[controller]")]
    public class SKUPicturesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SKUPicturesController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSKUPictureById(int id)
        {
            var skuPicture = await _context.SKUPictures.SingleOrDefaultAsync(x => x.Id == id);
            if (skuPicture == null) return NotFound(id);

            _context.SKUPictures.Remove(skuPicture);
            await _context.SaveChangesAsync();
            return Ok(id);
        }

        [HttpPut("{SKUPictures}")]
        public async Task<IActionResult> ReorderSKUPictures(int[] SKUPictures)
        {
            int order = 0;
            if (SKUPictures == null) return BadRequest("Null values sent");
            foreach (var skuPictureId in SKUPictures)
            {
                var skuPicture = await _context.SKUPictures.SingleOrDefaultAsync(x => x.Id == skuPictureId);
                if (skuPicture!= null)
                {
                    skuPicture.Sorting = order;
                }
                order++;
            }
            await _context.SaveChangesAsync();
            return Ok(SKUPictures);
        }
    }
}