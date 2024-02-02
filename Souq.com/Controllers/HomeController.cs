using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Souq.com.Data;
using Souq.com.Models;
using System.Diagnostics;

namespace Souq.com.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context= context;
        }

        public IActionResult Index()
        {var product = _context.products.ToList();
            return View(product);
        }
        public IActionResult Category()
        {
            var category = _context.categories.ToList();
            return View(category);
        }
        public IActionResult GetProductByCategoryId(int id)
        {
            var product = _context.products.Where(x => x.CategoryId == id).ToList();
               return View(product);
        }
        public IActionResult ProductSearch(string xname) 
        { var pro = _context.products.Where(x=>x.Name.Contains(xname)).ToList();

             return View(pro);
        }
        [HttpGet]
        public IActionResult Contact()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult SendReview(Review review)
        {
            _context.reviews.Add(new Review
            {
                Name = review.Name,
                Subject = review.Subject,
                Message = review.Message,
                Email = review.Email,
            });
            _context.SaveChanges();
            return RedirectToAction("Review");
        }

        public IActionResult Review()
        {
            var Review = _context.reviews.ToList();
            return View(Review);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ViewDetails(int id)
        {
            var product=_context.products.Include(x=>x.category).FirstOrDefault(x=>x.Id==id);
            return View(product);
        }
        public IActionResult FilterByPrice()
        {
            var product=_context.products.OrderBy(x=>x.Price).ToList();
            return View(product);
        }
        public IActionResult FilterByDate()
        {
            var product = _context.products.OrderByDescending(x => x.Date).ToList();
            return View(product);
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Faq()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}