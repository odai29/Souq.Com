using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Souq.com.Data;
using Souq.com.Models;

namespace Souq.com.Controllers
{
    [Authorize (Roles ="Admin")]
    public class ProductsAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductsAdminController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: ProductsAdmin
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.products.Include(p => p.category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.products == null)
            {
                return NotFound();
            }

            var product = await _context.products
                .Include(p => p.category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: ProductsAdmin/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Name");
            return View();
        }

        // POST: ProductsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price,Date,Image,CategoryId,ImageFile")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Imgs/" + fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);

                    }
                    product.Image = fileName;
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: ProductsAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.products == null)
            {
                return NotFound();
            }

            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: ProductsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Price,Date,Image,CategoryId,ImageFile")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Imgs/" + fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);

                    }
                    product.Image = fileName;
                }
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
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // GET: ProductsAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.products == null)
            {
                return NotFound();
            }

            var product = await _context.products
                .Include(p => p.category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: ProductsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.products'  is null.");
            }
            var product = await _context.products.FindAsync(id);
            if (product != null)
            {
                _context.products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
