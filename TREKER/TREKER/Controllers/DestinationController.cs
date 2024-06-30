using Microsoft.AspNetCore.Mvc;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.PageVM;

namespace TREKER.MVC.Controllers
{
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;
        private readonly IRegionService _regionService;

        public DestinationController(IDestinationService destinationService, IRegionService regionService)
        {
            _destinationService = destinationService;
            _regionService = regionService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new DestinationVM
            {
                Destinations = (await _destinationService.GetAllAsync()).Where(x => !x.IsDeleted),
                Regions = (await _regionService.GetAllAsync()).Where(x => !x.IsDeleted),
            };

            ViewBag.ActivePage = "Destination";
            return View(vm);
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
