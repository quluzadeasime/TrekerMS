using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.PageVM;
using TREKER.Core.Entities;
using TREKER.DAL.Exceptions.Common;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IActionResult> Table()
        {
            return View(await _contactRepository.GetAllAsync());
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactRepository.DeleteAsync(id);
            await _contactRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Table));

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Recover(int id)
        {
            await _contactRepository.RecoverAsync(id);
            await _contactRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Table));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            _contactRepository.Remove(id);
            await _contactRepository.SaveChangesAsync();

            return RedirectToAction(nameof(Table));
        }
    }
}
