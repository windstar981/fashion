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
    public class LnkProductAttributesController : Controller
    {
        private readonly FashionContext _context;

        public LnkProductAttributesController(FashionContext context)
        {
            _context = context;
        }

        // GET: LnkProductAttributes
        public async Task<IActionResult> Index()
        {
            var fashionContext = _context.LnkProductAttributes.Include(l => l.Attribute).Include(l => l.Product);
            return View(await fashionContext.ToListAsync());
        }

        // GET: LnkProductAttributes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lnkProductAttribute = await _context.LnkProductAttributes
                .Include(l => l.Attribute)
                .Include(l => l.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lnkProductAttribute == null)
            {
                return NotFound();
            }

            return View(lnkProductAttribute);
        }

        // GET: LnkProductAttributes/Create
        public IActionResult Create()
        {
            ViewData["AttributeId"] = new SelectList(_context.Attributes, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: LnkProductAttributes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,AttributeId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] LnkProductAttribute lnkProductAttribute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lnkProductAttribute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AttributeId"] = new SelectList(_context.Attributes, "Id", "Id", lnkProductAttribute.AttributeId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", lnkProductAttribute.ProductId);
            return View(lnkProductAttribute);
        }

        // GET: LnkProductAttributes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lnkProductAttribute = await _context.LnkProductAttributes.FindAsync(id);
            if (lnkProductAttribute == null)
            {
                return NotFound();
            }
            ViewData["AttributeId"] = new SelectList(_context.Attributes, "Id", "Id", lnkProductAttribute.AttributeId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", lnkProductAttribute.ProductId);
            return View(lnkProductAttribute);
        }

        // POST: LnkProductAttributes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,AttributeId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] LnkProductAttribute lnkProductAttribute)
        {
            if (id != lnkProductAttribute.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lnkProductAttribute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LnkProductAttributeExists(lnkProductAttribute.Id))
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
            ViewData["AttributeId"] = new SelectList(_context.Attributes, "Id", "Id", lnkProductAttribute.AttributeId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", lnkProductAttribute.ProductId);
            return View(lnkProductAttribute);
        }

        // GET: LnkProductAttributes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lnkProductAttribute = await _context.LnkProductAttributes
                .Include(l => l.Attribute)
                .Include(l => l.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lnkProductAttribute == null)
            {
                return NotFound();
            }

            return View(lnkProductAttribute);
        }

        // POST: LnkProductAttributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lnkProductAttribute = await _context.LnkProductAttributes.FindAsync(id);
            if (lnkProductAttribute != null)
            {
                _context.LnkProductAttributes.Remove(lnkProductAttribute);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LnkProductAttributeExists(int id)
        {
            return _context.LnkProductAttributes.Any(e => e.Id == id);
        }
    }
}
