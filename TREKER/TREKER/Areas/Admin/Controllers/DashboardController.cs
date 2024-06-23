using Microsoft.AspNetCore.Mvc;

namespace TREKER.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Tables()
        {
            return View();
        }
    }
}
