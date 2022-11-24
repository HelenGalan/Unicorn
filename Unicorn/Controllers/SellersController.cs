using Microsoft.AspNetCore.Mvc;

namespace Unicorn.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
