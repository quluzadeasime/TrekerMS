using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.TeamMemberVMs;
using TREKER.Business.ViewModels.TrekkingVMs;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Interfaces
{
    public interface ITrekkingService
    {
        Task<IQueryable<Trekking>> GetAllAsync();
        Task<Trekking> GetByIdAsync(int id);
        Task CreateAsync(CreateTrekkingVM vm);
        Task UpdateAsync(UpdateTrekkingVM vm);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
