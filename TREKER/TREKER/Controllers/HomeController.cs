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

        public IActionResult AccessDeniedCustom()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
      
    }
}
