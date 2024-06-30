using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using TREKER.Business.Services.Abstractions;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.DestinationVMs;
using TREKER.Business.ViewModels.TrekkingVMs;
using TREKER.Core.Entities;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TrekkingController : Controller
    {
        private readonly ITrekkingService _trekkingService;
        private readonly IDestinationService _destinationService;
        private readonly IDifficultyService _difficultyService;
        private readonly IFeatureService _featureService;
        private readonly IFacilityService _facilityService;
        private readonly IDayService _dayService;

        public TrekkingController(ITrekkingService trekkingService, IDestinationService destinationService, IDifficultyService difficultyService, 
            IFeatureService featureService, IFacilityService facilityService, IDayService dayService)
        {
            _trekkingService = trekkingService;
            _destinationService = destinationService;
            _difficultyService = difficultyService;
            _featureService = featureService;
            _facilityService = facilityService;
            _dayService = dayService;
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Table()
        {
            if (User.IsInRole("Admin"))
            {
                return View(await _trekkingService.GetAllAsync());
            }
            else
            {
                return View((await _trekkingService.GetAllAsync()).Where(x => !x.IsDeleted));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Create()
        {
            CreateTrekkingVM vm = new()
            {
                Features = await (await _featureService.GetAllAsync()).ToListAsync(),
                Facilities = await (await _facilityService.GetAllAsync()).ToListAsync(),
                Destinations = await (await _destinationService.GetAllAsync()).ToListAsync(),
                Difficulties = await (await _difficultyService.GetAllAsync()).ToListAsync(),
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Create(CreateTrekkingVM vm)
        {
            vm.Features = await (await _featureService.GetAllAsync()).ToListAsync();
            vm.Facilities = await(await _facilityService.GetAllAsync()).ToListAsync();
            vm.Destinations = await (await _destinationService.GetAllAsync()).ToListAsync();
            vm.Difficulties = await (await _difficultyService.GetAllAsync()).ToListAsync();

            CreateTrekkingVMValidator validations = new();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _trekkingService.CreateAsync(vm);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var oldTrekking = await _trekkingService.GetByIdAsync(id);

            UpdateTrekkingVM vm = new()
            {
                Title = oldTrekking.Title,
                SubTitle = oldTrekking.SubTitle,
                Price = oldTrekking.Price,
                Description = oldTrekking.Description,
                GroupSize = oldTrekking.GroupSize,
                ReviewCount = oldTrekking.ReviewCount,
                Duration = oldTrekking.Duration,
                RoadHeight = oldTrekking.RoadHeight,
                Star = oldTrekking.Star,
                OldImages = oldTrekking.Images,
                DestinationId = oldTrekking.DestinationId,
                DifficultyId = oldTrekking.DifficultyId,
                FeatureIds = oldTrekking.Features.Select(x => x.FeatureId).ToList(),
                FacilityIds = oldTrekking.Facilities.Select(x => x.FacilityId).ToList(),
                Features = await (await _featureService.GetAllAsync()).ToListAsync(),
                Facilities = await (await _facilityService.GetAllAsync()).ToListAsync(),
                Destinations = await (await _destinationService.GetAllAsync()).ToListAsync(),
                Difficulties = await (await _difficultyService.GetAllAsync()).ToListAsync(),
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Update(UpdateTrekkingVM vm)
        {
            var oldTrekking = await _trekkingService.GetByIdAsync(vm.Id);

            vm.Features = await (await _featureService.GetAllAsync()).ToListAsync();
            vm.Facilities = await (await _facilityService.GetAllAsync()).ToListAsync();
            vm.Destinations = await (await _destinationService.GetAllAsync()).ToListAsync();
            vm.Difficulties = await (await _difficultyService.GetAllAsync()).ToListAsync();
            vm.OldImages = oldTrekking.Images;
    
            UpdateTrekkingVMValidator validations = new UpdateTrekkingVMValidator();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _trekkingService.UpdateAsync(vm);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _trekkingService.DeleteAsync(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Detail(int id)
        {
            var trekking = await _trekkingService.GetByIdAsync(id);

            return View(trekking);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Recover(int id)
        {
            await _trekkingService.RecoverAsync(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            await _trekkingService.RemoveAsync(id);

            return RedirectToAction(nameof(Table));
        }
    }
}
