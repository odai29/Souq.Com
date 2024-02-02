using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Souq.com.Data;
using Souq.com.Models;

namespace Souq.com.Controllers
{
    
    public class FAQsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FAQsController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public IActionResult ViewFaq() 
        {
          var faq=_context.faq.ToList();
        return View(faq);
        }
        // GET: FAQs
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
              return _context.faq != null ? 
                          View(await _context.faq.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.faq'  is null.");
        }

        // GET: FAQs/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.faq == null)
            {
                return NotFound();
            }

            var fAQ = await _context.faq
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fAQ == null)
            {
                return NotFound();
            }

            return View(fAQ);
        }

        // GET: FAQs/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: FAQs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Question,Answer")] FAQ fAQ)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fAQ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fAQ);
        }

        // GET: FAQs/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.faq == null)
            {
                return NotFound();
            }

            var fAQ = await _context.faq.FindAsync(id);
            if (fAQ == null)
            {
                return NotFound();
            }
            return View(fAQ);
        }

        // POST: FAQs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Question,Answer")] FAQ fAQ)
        {
            if (id != fAQ.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fAQ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FAQExists(fAQ.Id))
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
            return View(fAQ);
        }

        // GET: FAQs/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.faq == null)
            {
                return NotFound();
            }

            var fAQ = await _context.faq
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fAQ == null)
            {
                return NotFound();
            }

            return View(fAQ);
        }

        // POST: FAQs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.faq == null)
            {
                return Problem("Entity set 'ApplicationDbContext.faq'  is null.");
            }
            var fAQ = await _context.faq.FindAsync(id);
            if (fAQ != null)
            {
                _context.faq.Remove(fAQ);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FAQExists(int id)
        {
          return (_context.faq?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
