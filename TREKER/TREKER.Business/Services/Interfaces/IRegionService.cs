using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.RegionVMs;
using TREKER.Business.ViewModels.TeamMemberVMs;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Interfaces
{
    public interface IRegionService
    {
        Task<IQueryable<Region>> GetAllAsync();
        Task<Region> GetByIdAsync(int id);
        Task CreateAsync(CreateRegionVM vm);
        Task UpdateAsync(UpdateRegionVM vm);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
