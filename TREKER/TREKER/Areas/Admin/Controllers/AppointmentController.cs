using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TREKER.Business.Services.Abstractions;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.AppointmentVMs;
using TREKER.Business.ViewModels.TeamMemberVMs;
using TREKER.Core.Enums;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ISendMessageService _sendMessageService;

        public AppointmentController(IAppointmentService appointmentService, ISendMessageService sendMessageService)
        {
            _appointmentService = appointmentService;
            _sendMessageService = sendMessageService;
        }

        [Authorize]
        public async Task<IActionResult> Table()
        {
            return View(await _appointmentService.GetAllAsync());
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var oldAppointment = await _appointmentService.GetByIdAsync(id);

            UpdateAppointmentVM vm = new()
            {
                Fullname = oldAppointment.Fullname,
                Email = oldAppointment.Email,
                Phone = oldAppointment.Phone,
                Date = oldAppointment.Date,
                IsFinished = oldAppointment.IsFinished,
                IsVerified = oldAppointment.IsVerified
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Update(UpdateAppointmentVM vm, string status1, string status2)
        {
            UpdateAppointmentVMValidator validations = new UpdateAppointmentVMValidator();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            vm.IsVerified = status1 == "option1" ? true : false;
            vm.IsFinished = status2 == "option1" ? true : false;

            await _appointmentService.UpdateAsync(vm);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Detail(int id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);

            return View(appointment);
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _appointmentService.DeleteAsync(id);

            return RedirectToAction(nameof(Table));

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Recover(int id)
        {
            await _appointmentService.RecoverAsync(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            await _appointmentService.RemoveAsync(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Verify(int id)
        {
            await _sendMessageService.SendQRMessageAsync(id);

            await _appointmentService.VerifyAppointment(id);

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        public async Task<IActionResult> Finish(int appointmentId)
        {
            await _appointmentService.FinishAppointment(appointmentId);

            return RedirectToAction(nameof(Finished));
        }

        [HttpGet]
        public IActionResult Finished()
        {
            return View();
        }
    }
}
