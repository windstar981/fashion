﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fashion.Data;
using System.Net.WebSockets;

namespace fashion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly FashionContext _context;

        public CategoriesController(FashionContext context)
        {
            _context = context;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Description,Img,Slug,ParentId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Category category, IFormFile? img)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // xử lí lưu file ảnh
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
        //        category.Img = "uploads/" + fileName;

        //        _context.Add(category);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(category);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(string Name, string Description, IFormFile? img, string Slug)
        //{
        //        // xử lí lưu file ảnh
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
        //    var category = new Category();
        //    category.Name = Name;
        //    category.Description = Description;
        //    category.Slug = Slug;
        //    category.Img = "uploads/" + fileName;

        //    _context.Add(category);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory()
        {
            var Name = HttpContext.Request.Form["Name"];
            var Slug = HttpContext.Request.Form["Slug"];
            var Description = HttpContext.Request.Form["Description"];
            var img = HttpContext.Request.Form.Files.FirstOrDefault();
            // xử lí lưu file ảnh
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
            var category = new Category { Name=Name, Description = Description, Slug = Slug, Img= "uploads/" + fileName };
            //category.Name = Name;
            //category.Description = Description;
            //category.Slug = Slug;
            //category.Img = "uploads/" + fileName;

            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Img,Slug,ParentId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Category category, IFormFile? img)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Nếu như người dùng thay đổi ảnh thì update Img theo url mới của ảnh mới
                    // Nếu không thì để nguyên url cũ
                    if (img != null && img.Length > 0)
                    {
                        // Lưu tập tin vào ổ đĩa hoặc xử lý tùy ý
                        var fileName = Path.GetFileName(img.FileName);
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            img.CopyTo(stream);
                        }
                        category.Img = "Uploads/" + fileName;
                    }
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
