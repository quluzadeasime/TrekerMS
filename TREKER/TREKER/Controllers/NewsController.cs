using Microsoft.AspNetCore.Mvc;

namespace TREKER.MVC.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
