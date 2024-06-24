using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.AccountVMs;
using TREKER.Core.Entities.UserModels;

namespace TREKER.Business.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AppUser> GetUserByEmailAddress(string emailAddress);
        Task RegisterAsync(RegisterVM vm);
        Task LoginAsync(LoginVM vm);
        Task LogoutAsync();
        Task CreateRoleAsync();
    }
}
