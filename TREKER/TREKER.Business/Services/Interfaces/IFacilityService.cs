using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.FacilityVMs;
using TREKER.Business.ViewModels.TeamMemberVMs;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Interfaces
{
    public interface IFacilityService
    {
        Task<IQueryable<Facility>> GetAllAsync();
        Task<Facility> GetByIdAsync(int id);
        Task CreateAsync(CreateFacilityVM vm);
        Task UpdateAsync(UpdateFacilityVM vm);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
