using Microsoft.AspNetCore.Mvc;

namespace TREKER.MVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
