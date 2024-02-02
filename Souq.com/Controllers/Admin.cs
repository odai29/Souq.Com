using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Souq.com.Controllers
{
    public class Admin : Controller
    {
        [Authorize (Roles="Admin")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
