using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Interfaces
{
    public interface ISendMessageService
    {
        void SendUrlMessage(string toUser, string member, string url);
        Task SendUrlMessageAsync(string userEmailAddress, string url);
        Task<string> GenerateTokenAsync(string currentUserUdOrName);
        Task ConfirmEmailAddress(string userEmailAddress,string token);
        Task<string> GenerateUserPasswordResetTokenAsync(string userEmailAddress);
        Task ConfirmChangePassword(string userEmailAddress, string token, string password);
        Task SendQRMessageAsync(int appointmentId);
        Task SendQRCodeMessageAsync(string toUser, Appointment appointment, string locationValue, string emailValue, string phoneValue, DateTime time);
    }
}
