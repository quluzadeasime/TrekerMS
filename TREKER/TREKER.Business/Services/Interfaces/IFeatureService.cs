using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.FeatureVMs;
using TREKER.Business.ViewModels.TeamMemberVMs;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Interfaces
{
    public interface IFeatureService
    {
        Task<IQueryable<Feature>> GetAllAsync();
        Task<Feature> GetByIdAsync(int id);
        Task CreateAsync(CreateFeatureVM vm);
        Task UpdateAsync(UpdateFeatureVM vm);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
