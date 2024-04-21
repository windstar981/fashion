using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fashion.Data;
using System.Web;
using System.Collections;
namespace fashion.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly FashionContext _context;

        public ProductsController(FashionContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var fashionContext = _context.Products.Include(p => p.Brand);
            return View(/*await fashionContext.ToListAsync()*/);
        }

        // GET: Admin/Products/Add_Products
        public async Task<IActionResult> Add_Product()
        {
            var fashionContext = _context.Products.Include(p => p.Brand);
            return View(/*await fashionContext.ToListAsync()*/);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = _context.Brands.ToList();
            ViewData["CategoryId"] = _context.Categories.ToList();
            var dataAttr = _context.Attributes.Where(a => a.ParentId == 0).ToList();
            ViewData["AllAttr"] = dataAttr;
            foreach (var data in dataAttr)
            {
                ViewData[data.Name] = _context.Attributes.Where(a => a.ParentId == data.Id);
            }
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Abstract,Description,Img,Slug,Quantity,Status,Deleted,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,BrandId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", product.BrandId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", product.BrandId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Abstract,Description,Img,Slug,Quantity,Status,Deleted,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,BrandId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", product.BrandId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Test(Product product, List<IFormFile> files)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Xử lý tải lên file (nếu cần)
        //        foreach (var file in files)
        //        {
        //            // Lưu file vào thư mục hoặc lưu trữ tùy thuộc vào yêu cầu
        //        }

        //        // Lưu sản phẩm vào cơ sở dữ liệu
        //        _context.Products.Add(product);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(product);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Test(IFormCollection form)
        //{
        //    string s = "abc";
        //    var files = HttpContext.Request.Form.Files;
        //    string tenTest = form["ten_test"];
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public async Task<IActionResult> Test()
        {
            // Xử lý dữ liệu của product và file_upload ở đây
            var name = HttpContext.Request.Form["name"];
            var slug = HttpContext.Request.Form["slug"];
            var abstracts = HttpContext.Request.Form["abstract"];
            var desc = HttpContext.Request.Form["desc"];
            var price = HttpContext.Request.Form["price"];
            var quantity = HttpContext.Request.Form["quantity"];
            var brand = HttpContext.Request.Form["brands"];
            string category = HttpContext.Request.Form["categories"];
            var files = HttpContext.Request.Form.Files;

            var size = HttpContext.Request.Form["size"];
            var color = HttpContext.Request.Form["color"];
            var dataAttr = _context.Attributes.Where(a => a.ParentId == 0).ToList();
            Dictionary<string, string> listInputAttr = new Dictionary<string, string>();
            foreach(var data in dataAttr)
            {
                listInputAttr.Add(data.Name, HttpContext.Request.Form[data.Name]);
            }



            // lưu dữ liệu vào db
            // cần lưu những bảng nào:
            // 1. Product, 2.  lnk_product_attribute, 3. lnk_product_category
            // lưu bảng product
            // nên validate dữ liệu trước khi lưu => bạn tự validate
            var url = "";
            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    // Lưu tập tin vào ổ đĩa hoặc xử lý tùy ý
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    url += "Uploads/" + fileName + ",";
                }
            }
            var product = new Product();
            product.Name = name;
            product.Slug = slug;
            product.Abstract = abstracts;
            product.Description = desc;
            product.Price = Convert.ToInt32(price);
            product.Quantity = Convert.ToInt32(quantity);
            product.BrandId = Convert.ToInt32(brand);
            product.Img = url;

            _context.Add(product);
            _context.SaveChanges();

            // lưu bảng lnk_product_attribute và lnk_product_category
            var ProductId = product.Id;
            string[] parts = category.Split(',');

            var lnkProductCategory = new List<LnkProductCategory>();

            for (int i = 0; i < parts.Length; i++)
            {
                var lnk = new LnkProductCategory();
                lnk.ProductId = ProductId;
                lnk.CategoryId = Convert.ToInt32(parts[i]);
                lnkProductCategory.Add(lnk);
            }
            _context.LnkProductCategories.AddRange(lnkProductCategory);
            _context.SaveChanges();

            var lnkProductAttribute = new List<LnkProductAttribute>();

            foreach (var key in listInputAttr.Keys)
            {
                var value = listInputAttr[key];
                string[] attrValues = value.Split(',');
                for (int i = 0; i < attrValues.Length; i++)
                {
                    var lnkAttr = new LnkProductAttribute();
                    lnkAttr.ProductId = ProductId;
                    lnkAttr.AttributeId = Convert.ToInt32(attrValues[i]);
                    lnkProductAttribute.Add(lnkAttr);
                }
            }
            _context.LnkProductAttributes.AddRange(lnkProductAttribute);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
