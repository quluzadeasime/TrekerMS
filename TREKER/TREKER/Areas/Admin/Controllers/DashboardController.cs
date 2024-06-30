using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.PageVM;

namespace TREKER.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ITeamMemberService _teamMemberService;
        private readonly IRegionService _regionService;
        private readonly ITrekkingService _trekkingService;
        private readonly IDestinationService _destinationService;
        private readonly IDifficultyService _difficultyService;
        private readonly IFeatureService _featureService;
        private readonly IFacilityService _facilityService;
        private readonly IBlogService _blogService;

        public DashboardController(ITeamMemberService teamMemberService, IRegionService regionService,
            ITrekkingService trekkingService, IDestinationService destinationService, IDifficultyService difficultyService,
            IFeatureService featureService, IFacilityService facilityService, IBlogService blogService)
        {
            _teamMemberService = teamMemberService;
            _regionService = regionService;
            _trekkingService = trekkingService;
            _destinationService = destinationService;
            _difficultyService = difficultyService;
            _featureService = featureService;
            _facilityService = facilityService;
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Tables()
        {
            var tables = new TablesVM
            {
                TeamMembers = (await _teamMemberService.GetAllAsync()).Where(x => !x.IsDeleted).Take(3),
                Blogs = (await _blogService.GetAllAsync()).Where(x => !x.IsDeleted).Take(3),
                Destinations = (await _destinationService.GetAllAsync()).Where(x => !x.IsDeleted).Take(3),
                Difficulties = (await _difficultyService.GetAllAsync()).Where(x => !x.IsDeleted).Take(3),
                Features = (await _featureService.GetAllAsync()).Where(x => !x.IsDeleted).Take(3),
                Trekkings = (await _trekkingService.GetAllAsync()).Where(x => !x.IsDeleted).Take(3),
                Facilities = (await _facilityService.GetAllAsync()).Where(x => !x.IsDeleted).Take(3),
                Regions = (await _regionService.GetAllAsync()).Where(x => !x.IsDeleted).Take(3),
            };
            return View(tables);
        }
    }
}