using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.BlogVMs;
using TREKER.Business.ViewModels.TeamMemberVMs;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IQueryable<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task CreateAsync(CreateBlogVM vm);
        Task UpdateAsync(UpdateBlogVM vm);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
