using Microsoft.AspNetCore.Mvc;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.ContactVMs;

namespace TREKER.MVC.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IContactService _contactService;

        public ContactUsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult Create(int? sended)
        {
            ViewBag.ActivePage = "Contact";

            if (sended == 1) ViewData["message"] = "Your message successfully sended!";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactVM vm)
        {
            ContactVMValidator validations = new ContactVMValidator();
            var validationResult = await validations.ValidateAsync(vm);

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _contactService.CreateAsync(vm);

            return Redirect("/ContactUs/Create?sended=1");
        }

    }
}
