using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREKER.Business.Services.Interfaces
{
    public interface ISendMessageService
    {
        void SendUrlMessage(string toUser, string member, string url);
        Task SendUrlMessageAsync(string userEmailAddress, string url);
        Task<string> GenerateTokenAsync(string currentUserUdOrName);
        Task ConfirmEmailAddress(string userEmailAddress,string token);
    }
}
