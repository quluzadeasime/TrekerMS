using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using TREKER.Business.Services.Interfaces;

namespace TREKER.MVC.Controllers
{
    public class NewsController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IDestinationService _destinationService;

        public NewsController(IBlogService blogService, IDestinationService destinationService)
        {
            _blogService = blogService;
            _destinationService = destinationService;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = (await _blogService.GetAllAsync()).Where(x => !x.IsDeleted);
            ViewBag.ActivePage = "News";
            ViewData["destinations"] = (await _destinationService.GetAllAsync()).Where(x => !x.IsDeleted);

            return View(blogs);
        }
    }
}
