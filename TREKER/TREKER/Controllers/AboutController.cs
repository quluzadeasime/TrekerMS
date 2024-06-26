using Microsoft.AspNetCore.Mvc;

namespace TREKER.MVC.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
