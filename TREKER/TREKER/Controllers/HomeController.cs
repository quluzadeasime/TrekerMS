using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace TREKER.Controllers
{
    public class HomeController : Controller
    {
       public IActionResult Index()
        {
            return View();
        }
        public IActionResult Destinations()
        {
            return View();
        }
        public IActionResult Trekking()
        {
            return View();
        }
        public IActionResult News()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
      
    }
}
