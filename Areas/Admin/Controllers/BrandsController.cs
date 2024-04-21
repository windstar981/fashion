using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fashion.Data;

namespace fashion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly FashionContext _context;

        public BrandsController(FashionContext context)
        {
            _context = context;
        }

        // GET: Admin/Brands
        public async Task<IActionResult> Index()
        {
            return View(await _context.Brands.ToListAsync());
        }

        // GET: Admin/Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Admin/Brands/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Brand brand, IFormFile img)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var fileName = "";
        //        if (img != null && img.Length > 0)
        //        {
        //            // Lưu tập tin vào ổ đĩa hoặc xử lý tùy ý
        //            fileName = Path.GetFileName(img.FileName);
        //            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
        //            using (var stream = new FileStream(path, FileMode.Create))
        //            {
        //                img.CopyTo(stream);
        //            }
        //        }
        //        brand.Img = "uploads/" + fileName;
        //        _context.Add(brand);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateBrand()
        //{
        //    var img = HttpContext.Request.Form.Files.FirstOrDefault();

        //    var fileName = "";
        //    if (img != null && img.Length > 0)
        //    {
        //        // Lưu tập tin vào ổ đĩa hoặc xử lý tùy ý
        //        fileName = Path.GetFileName(img.FileName);
        //        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
        //        using (var stream = new FileStream(path, FileMode.Create))
        //        {
        //            img.CopyTo(stream);
        //        }
        //    }
        //    var name = HttpContext.Request.Form["Name"];
        //    var desc = HttpContext.Request.Form["Description"];
        //    var slug = HttpContext.Request.Form["Slug"];

        //    var brand = new Brand();
        //    brand.Name = name;
        //    brand.Description = desc;
        //    brand.Slug = slug;
        //    brand.Img = "Uploads/" + fileName;

        //    //brand.Img = "uploads/" + fileName;
        //    _context.Add(brand);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                var img = HttpContext.Request.Form.Files.FirstOrDefault();
                var fileName = "";
                if (img != null && img.Length > 0)
                {
                    // Lưu tập tin vào ổ đĩa hoặc xử lý tùy ý
                    fileName = Path.GetFileName(img.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        img.CopyTo(stream);
                    }
                }
                brand.Img = "uploads/" + fileName;
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Admin/Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Brand brand, IFormFile? picture, string CurrentImgPath)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(picture != null && picture.Length > 0)
                    {
                        brand.Img = "Uploads/" + Path.GetFileName(picture.FileName);
                    }
                    else
                    {
                        brand.Img = CurrentImgPath;
                    }
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
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
            return View(brand);
        }

        // GET: Admin/Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Admin/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }
}
