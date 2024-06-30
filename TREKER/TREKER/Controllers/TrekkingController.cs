using Microsoft.AspNetCore.Mvc;
using TREKER.Business.Services.Abstractions;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.AppointmentVMs;
using TREKER.Business.ViewModels.PageVM;
using TREKER.Business.ViewModels.TeamMemberVMs;
using TREKER.Core.Enums;

namespace TREKER.MVC.Controllers
{
    public class TrekkingController : Controller
    {
        private readonly ITrekkingService _trekkingService;
        private readonly IDifficultyService _difficultyService;
        private readonly IDestinationService _destinationService;
        private readonly IAppointmentService _appointmentService;

        public TrekkingController(ITrekkingService trekkingService, IDifficultyService difficultyService, IDestinationService destinationService, IAppointmentService appointmentService)
        {
            _trekkingService = trekkingService;
            _difficultyService = difficultyService;
            _destinationService = destinationService;
            _appointmentService = appointmentService;
        }

        public async Task<IActionResult> Index(int? destinationId)
        {
            var vm = new TrekkingVM
            {
                Difficulties = (await _difficultyService.GetAllAsync()).Where(x => !x.IsDeleted)
            };

            if(destinationId is not null)
            {
                vm.Trekkings = (await _trekkingService.GetAllAsync()).Where(x => !x.IsDeleted).Where(x => x.DestinationId == destinationId);
            }
            else
            {
                vm.Trekkings = (await _trekkingService.GetAllAsync()).Where(x => !x.IsDeleted);
            }

            ViewBag.ActivePage = "Trekking";
            
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var oldTrekking = await _trekkingService.GetByIdAsync(id);


			var vm = new CreateAppointmentVM    
            {
                Trekking = oldTrekking,
                Trekkings = (await _trekkingService.GetAllAsync()).Where(x => !x.IsDeleted),
                Destinations = (await _destinationService.GetAllAsync()).Where(x => !x.IsDeleted)
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(CreateAppointmentVM vm, int id)
        {
            var oldTrekking = await _trekkingService.GetByIdAsync(id);

            CreateAppointmentVMValidator validations = new CreateAppointmentVMValidator();
            var validationResult = await validations.ValidateAsync(vm);

            vm.Trekking = oldTrekking;
            vm.Trekkings = (await _trekkingService.GetAllAsync()).Where(x => !x.IsDeleted);
            vm.Destinations = (await _destinationService.GetAllAsync()).Where(x => !x.IsDeleted);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _appointmentService.CreateAsync(vm);

            return Redirect($"/Trekking/Alert?id={id}");
        }

        public IActionResult Alert(int id)
        {
            return View(id);
        }
    }
}
