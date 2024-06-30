using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TREKER.Business.Services.Interfaces;
namespace TREKER.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITrekkingService _trekkingService;

        public HomeController(ITrekkingService trekkingService)
        {
            _trekkingService = trekkingService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ActivePage = "Home";
            return View((await _trekkingService.GetAllAsync()).Where(x => !x.IsDeleted));
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
