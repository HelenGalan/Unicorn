using Microsoft.AspNetCore.Mvc;

namespace Unicorn.Controllers
{
    public class SalesRecordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Simplesearch()
        {
            return View();
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }


    }
}
