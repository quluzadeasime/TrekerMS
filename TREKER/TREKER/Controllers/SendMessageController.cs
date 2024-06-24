using Microsoft.AspNetCore.Mvc;
using TREKER.Business.Services.Interfaces;

namespace TREKER.MVC.Controllers
{
    public class SendMessageController : Controller
    {
        private readonly ISendMessageService _sendMessageService;

        public SendMessageController(ISendMessageService sendMessageService)
        {
            _sendMessageService = sendMessageService;
        }

        public async Task<IActionResult> ConfirmEmailAddress(string emailAddress, string token)
        {
            if (!Request.Cookies.ContainsKey("ConfirmationLinkSent"))
            {
                return RedirectToAction(nameof(ConfirmationCookieExpired));
            }

            await _sendMessageService.ConfirmEmailAddress(emailAddress, token);

            Response.Cookies.Delete("ConfirmationLinkSent");

            return RedirectToAction("Login", "Account");
        }

        public IActionResult ConfirmationCookieExpired()
        {
            return View();
        }
    }
}
