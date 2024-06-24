using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Packaging.Signing;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.AccountVMs;
using TREKER.Core.Entities.UserModels;
using TREKER.DAL.Exceptions.Account;

namespace TREKER.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _http;
        private readonly ISendMessageService _sendMessageService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(IAccountService accountService, LinkGenerator linkGenerator = null, IHttpContextAccessor http = null, ISendMessageService sendMessageService = null, UserManager<AppUser> userManager = null, SignInManager<AppUser> signInManager = null)
        {
            _accountService = accountService;
            _linkGenerator = linkGenerator;
            _http = http;
            _sendMessageService = sendMessageService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult CheckEmailAddress()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            RegisterVMValidator validator = new RegisterVMValidator();
            var validationResult = await validator.ValidateAsync(vm);
            var emailAddress = vm.Email;

            if (!validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }

            await _accountService.RegisterAsync(vm);

            var token = await _sendMessageService.GenerateTokenAsync(emailAddress);

            string url = _linkGenerator.GetUriByAction(_http.HttpContext, action: "ConfirmEmailAddress", controller: "SendMessage",
            values: new
            {
                token,
                emailAddress
            });

            await _sendMessageService.SendUrlMessageAsync(emailAddress, url);

            Response.Cookies.Append("ConfirmationLinkSent", "true", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddMinutes(30)
            });

            return RedirectToAction(nameof(CheckEmailAddress));
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            await _accountService.LogoutAsync();

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin");
            }
            else
            {
                await _accountService.CreateRoleAsync();
                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            LoginVMValidator validations = new LoginVMValidator(_userManager, _signInManager);
            var validationResult = await validations.ValidateAsync(vm);

            if (validationResult.IsValid)
            {
                ModelState.Clear();

                validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));

                return View(vm);
            }
            await _accountService.LoginAsync(vm);

            return Redirect("/Admin");
        }
    }
}
