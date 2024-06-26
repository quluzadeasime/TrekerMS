using Microsoft.AspNetCore.Mvc;

namespace TREKER.MVC.Controllers
{
    public class TrekkingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
