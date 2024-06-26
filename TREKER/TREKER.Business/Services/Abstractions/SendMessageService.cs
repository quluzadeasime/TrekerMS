using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Core.Entities.UserModels;

namespace TREKER.Business.Services.Abstractions
{
    public class SendMessageService : ISendMessageService
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;

        public SendMessageService(IAccountService accountService, UserManager<AppUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        public async Task ConfirmChangePassword(string userEmailAddress, string token, string password)
        {
            var oldUser = await _accountService.GetUserByEmailAddress(userEmailAddress);

            await _userManager.ResetPasswordAsync(oldUser, token, password);
        }

        public async Task ConfirmEmailAddress(string userEmailAddress, string token)
        {
            var oldUser = await _accountService.GetUserByEmailAddress(userEmailAddress);

            await _userManager.ConfirmEmailAsync(oldUser, token);
        }

        public async Task<string> GenerateTokenAsync(string userEmailAddress)
        {
            var currentUser = await _accountService.GetUserByEmailAddress(userEmailAddress);

            return await _userManager.GenerateEmailConfirmationTokenAsync(currentUser);
        }

        public async Task<string> GenerateUserPasswordResetTokenAsync(string userEmailAddress)
        {
            var currentUser = await _accountService.GetUserByEmailAddress(userEmailAddress);

            return await _userManager.GeneratePasswordResetTokenAsync(currentUser);
        }

        public void SendUrlMessage(string toUser, string member, string url)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential("sendm554@gmail.com", "qeyc prgm qoro xfel");
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("sendm554@gmail.com"),
                    Subject = "Welcome to Treker Team",
                    Body = $"<!DOCTYPE html>" +
                       $"<html>" +
                       $"<head>" +
                       $"<style>" +
                       $"  body {{" +
                       $"    font-family: 'Arial', sans-serif;" +
                       $"    background-color: #f4f4f4;" +
                       $"    margin: 0;" +
                       $"    padding: 0;" +
                       $"  }}" +
                       $"  .container {{" +
                       $"    max-width: 600px;" +
                       $"    margin: auto;" +
                       $"    padding: 20px;" +
                       $"    background-color: #ffffff;" +
                       $"    border-radius: 5px;" +
                       $"    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);" +
                       $"  }}" +
                       $"  h1 {{" +
                       $"    color: #333333;" +
                       $"  }}" +
                       $"  p {{" +
                       $"    color: #666666;" +
                       $"  }}" +
                       $"</style>" +
                       $"</head>" +
                       $"<body>" +
                       $"  <div class='container'>" +
                       $"    <h1>Welcome to Treker, {member}!</h1>" +
                       $"    <p>This is your confirmation url for change password verification:</p>" +
                       $"    <h2 style='color: #007BFF;'><a href=\"{url}\">Click Me!</a></h2>" +
                       $"  </div>" +
                       $"</body>" +
                       $"</html>",
                    IsBodyHtml = true
                };


                mailMessage.To.Add(toUser);
                client.Send(mailMessage);
            }
        }

        public async Task SendUrlMessageAsync(string userEmailAddress, string url)
        {
            var currentUser = await _accountService.GetUserByEmailAddress(userEmailAddress);

            SendUrlMessage(
                toUser: currentUser.Email,
                member: $"{currentUser.FirstName} {currentUser.LastName}",
                url: url
                );
        }
    }
}
