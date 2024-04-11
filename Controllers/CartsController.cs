using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fashion.Data;

namespace fashion.Controllers
{
    public class CartsController : Controller
    {
        private readonly FashionContext _context;

        public CartsController(FashionContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("IdCustomer") == null)
            {
                //Đã đăng nhập, chuyển hướng đến trang chính
                return RedirectToAction("Login","Home");
            }
            var cusId = HttpContext.Session.GetInt32("IdCustomer");
            //Console.WriteLine(cusId);
            var cart = _context.Carts.Include(c => c.Product).Where(c => c.CustomerId == cusId);
            ViewBag.ListProductInCart = cart;
            return View(/*await fashionContext.ToListAsync()*/);
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Customer)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,Slug,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,ProductId,CustomerId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", cart.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cart.ProductId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", cart.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cart.ProductId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity,Slug,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,ProductId,CustomerId")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", cart.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", cart.ProductId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Customer)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        [HttpGet]
        public async Task<IActionResult> AddToCart(int id, string type = "plus")
        {
            if (HttpContext.Session.GetInt32("IdCustomer") == null)
            {
                // Đã đăng nhập, chuyển hướng đến trang chính
                return Json(new { success = false, content = "Vui lòng đăng nhập để thêm sản phẩm vào giỏ hàng!!" }); 
            }
            if (id == null)
            {
                return NotFound();
            }
            var UserId = HttpContext.Session.GetInt32("IdCustomer");
            var cart = _context.Carts.FirstOrDefault(c => c.ProductId == id && c.CustomerId == UserId);
            if(cart is not null)
            {
                if(type.Equals("plus"))
                {
                    cart.Quantity = cart.Quantity + 1;
                }
                else
                {
                    cart.Quantity = cart.Quantity - 1;
                }
                _context.SaveChanges();
                return Json(new { success = true });
            }
            else
            {
                var new_cart = new Cart();
                new_cart.CustomerId = HttpContext.Session.GetInt32("IdCustomer");
                new_cart.Quantity = 1;
                new_cart.ProductId = id;
                _context.Carts.Add(new_cart);
                if (_context.SaveChanges() == 1)
                {
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            
            //return RedirectToAction("Index", "Home");

        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }

    }
}
