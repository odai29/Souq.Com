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
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Carts
        [Authorize]
        public async Task<IActionResult> Index()
        {
            
            var applicationDbContext = _context.carts.Include(c => c.product).Where(x=>x.UserId== User.Identity.Name);
            return View(await applicationDbContext.ToListAsync());
        }
        [HttpGet]
        public IActionResult GoOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            Order o=new Order{
                Email=order.Email,
                Address=order.Address,
                Name=order.Name,
                PhoneNumber=order.PhoneNumber,
                UserId=User.Identity.Name
            };
            var cartItem=_context.carts.Where(x=> x.UserId==User.Identity.Name).ToList();
            _context.carts.RemoveRange(cartItem);
            _context.orders.Add(o);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult AddProToCart(int id)
        {
            var price = _context.products.Find(id).Price;
            var item = _context.carts.FirstOrDefault(x => x.ProductId == id && x.UserId == User.Identity.Name);
            if (item != null)
            {
                item.Quentity += 1;
            }
            else
            {
                _context.carts.Add(new Cart
                {
                    ProductId = id,
                    Price = price,
                    UserId = User.Identity.Name,
                    Quentity = 1,
                    Date = DateTime.Now
                }); 

            }
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.carts == null)
            {
                return NotFound();
            }

            var cart = await _context.carts
                .Include(c => c.product)
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
            ViewData["ProductId"] = new SelectList(_context.products, "Id", "Id");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Price,Quentity,UserId,ProductId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.products, "Id", "Id", cart.ProductId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.carts == null)
            {
                return NotFound();
            }

            var cart = await _context.carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.products, "Id", "Id", cart.ProductId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Price,Quentity,UserId,ProductId")] Cart cart)
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
            ViewData["ProductId"] = new SelectList(_context.products, "Id", "Id", cart.ProductId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.carts == null)
            {
                return NotFound();
            }

            var cart = await _context.carts
                .Include(c => c.product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.carts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.carts'  is null.");
            }
            var cart = await _context.carts.FindAsync(id);
            if (cart != null)
            {
                _context.carts.Remove(cart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
          return (_context.carts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
