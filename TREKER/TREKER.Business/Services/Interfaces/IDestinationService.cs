using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.DestinationVMs;
using TREKER.Business.ViewModels.TeamMemberVMs;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Interfaces
{
    public interface IDestinationService
    {
        Task<IQueryable<Destination>> GetAllAsync();
        Task<Destination> GetByIdAsync(int id);
        Task CreateAsync(CreateDestinationVM vm);
        Task UpdateAsync(UpdateDestinationVM vm);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
