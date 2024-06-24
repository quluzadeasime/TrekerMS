using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.TeamMemberVMs;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Interfaces
{
    public interface ITeamMemberService
    {
        Task<IQueryable<TeamMember>> GetAllAsync();
        Task<TeamMember> GetByIdAsync(int id);
        Task CreateAsync(CreateTeamMemberVm vm);
        Task UpdateAsync(UpdateTeamMemberVm vm);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}