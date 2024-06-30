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
using TREKER.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using TREKER.Core.Entities;
using QRCoder;

namespace TREKER.Business.Services.Abstractions
{
    public class SendMessageService : ISendMessageService
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppointmentService _appointmentService;
        private readonly ISettingService _settingService;

        public SendMessageService(IAccountService accountService, UserManager<AppUser> userManager, IAppointmentService appointmentService, ISettingService settingService)
        {
            _accountService = accountService;
            _userManager = userManager;
            _appointmentService = appointmentService;
            _settingService = settingService;
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

            oldUser.IsRegistered = true;

            await _userManager.UpdateAsync(oldUser);
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

        public async Task SendQRCodeMessageAsync(string toUser, Appointment appointment, string locationValue, string emailValue, string phoneValue, DateTime time)
        {
            string finish = $"https://preferably-large-grouper.ngrok-free.app/Admin/Appointment/Finish?appointmentId={appointment.Id}";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(finish, QRCodeGenerator.ECCLevel.Q);
            using (var qrCode = new PngByteQRCode(qrCodeData))
            {
                byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);
                string qrCodeBase64 = Convert.ToBase64String(qrCodeAsPngByteArr);

                if (string.IsNullOrEmpty(qrCodeBase64))
                {
                    throw new InvalidOperationException("QR code generation failed.");
                }

                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.UseDefaultCredentials = false;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new NetworkCredential("sendm554@gmail.com", "qeyc prgm qoro xfel");
                    client.EnableSsl = true;

                    using (MemoryStream ms = new MemoryStream(qrCodeAsPngByteArr))
                    {
                        Attachment qrCodeAttachment = new Attachment(ms, "QRCode.png", "image/png");

                        var mailMessage = new MailMessage
                        {
                            From = new MailAddress("sendm554@gmail.com"),
                            Subject = "Welcome to Treker",
                            Body = $@"<!DOCTYPE html>
                            <html>
                            <head>
                                <style>
                                    body {{
                                        font-family: 'Arial', sans-serif;
                                        background-color: #f4f4f4;
                                        margin: 0;
                                        padding: 20px;
                                    }}
                                    .container {{
                                        max-width: 600px;
                                        margin: auto;
                                        background-color: #ffffff;
                                        border-radius: 5px;
                                        box-shadow: 0 2px 5px rgba(0,0,0,0.1);
                                        padding: 20px;
                                        text-align: center;
                                    }}
                                    h1 {{
                                        color: #333;
                                    }}
                                    p {{
                                        color: #666;
                                        font-size: 16px;
                                        line-height: 1.6;
                                    }}
                                    .qr-code {{
                                        margin: 20px auto;
                                        padding: 15px;
                                        background-color: #f9f9f9;
                                        border-radius: 5px;
                                        display: inline-block;
                                    }}
                                    .qr-code img {{
                                        width: 140px;
                                        height: 140px;
                                    }}
                                    .hospital-info {{
                                        margin-top: 30px;
                                        border-top: 1px solid #ccc;
                                        padding-top: 20px;
                                        text-align: left;
                                    }}
                                    .hospital-info h5 {{
                                        color: #333;
                                        margin-bottom: 10px;
                                    }}
                                    .hospital-info h6 {{
                                        color: #666;
                                        margin: 5px 0;
                                    }}
                                </style>
                            </head>
                            <body>
                                <div class='container'>
                                    <h1>Welcome to Treker, {appointment.Fullname}!</h1>
                                    <p>Please scan the QR code below to check-in for your appointment.</p>
                                    <div class='qr-code'>
                                        <img src='cid:QRCode.png' alt='Check-in QR Code'>
                                    </div>
                                    <p>
                                        Please come to the reception and complete your appointment by reading the QR code. 
                                        After the appointment, you will receive a link to the payment section.
                                        If you have any questions, don't hesitate to contact us.
                                    </p>                                    
                                    <p id=""message"" style=""color: red; font-weight: bold;"">
                                        Please arrive in {time.ToString("dddd, MMMM dd, yyyy 'at' hh:mm tt")}, your appointment to ensure timely check-in.
                                    </p>                                    
                                    <div class='hospital-info'>
                                        <h5>Treker Information:</h5>
                                        <h6><strong>Location:</strong> {locationValue}</h6>
                                        <h6><strong>Email:</strong> {emailValue}</h6>
                                        <h6><strong>Phone:</strong> {phoneValue}</h6>
                                    </div>
                                </div>
                            </body>

                            </html>",
                            IsBodyHtml = true
                        };

                        qrCodeAttachment.ContentId = "QRCode.png";
                        qrCodeAttachment.ContentDisposition.Inline = true;
                        mailMessage.Attachments.Add(qrCodeAttachment);

                        mailMessage.To.Add(toUser);
                        try
                        {
                            await client.SendMailAsync(mailMessage);
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException("Failed to send email.", ex);
                        }
                    }
                }
            }
        }

        public async Task SendQRMessageAsync(int appointmentId)
        {
            var oldAppointment = await _appointmentService.GetByIdAsync(appointmentId);
            var settings = await (await _settingService.GetAllAsync()).FirstOrDefaultAsync();

            SendQRCodeMessageAsync(
                toUser: oldAppointment.Email,
                appointment: oldAppointment,
                emailValue: settings.Email,
                phoneValue: settings.Phone1,
                locationValue: settings.Address1,
                time: oldAppointment.Date
                );
        }
    }
}
