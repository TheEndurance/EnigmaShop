using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnigmaShop.Areas.Admin.Controllers
{
    public class SKUPicturesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SKUPicturesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpPut]
        public async Task<IActionResult> ReorderSKUPictures(int[] SKUPictures)
        {
            int order = 0;
            if (SKUPictures == null) return BadRequest("Null values sent");
            foreach (var skuPictureId in SKUPictures)
            {
                var skuPicture = await _context.SKUPictures.SingleOrDefaultAsync(x => x.Id == skuPictureId);
                if (skuPicture != null)
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