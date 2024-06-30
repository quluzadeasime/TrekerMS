using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.DayVMs;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Interfaces
{
    public interface IDayService
    {
        Task<IQueryable<Day>> GetAllAsync();
        Task<Day> GetByIdAsync(int id);
        Task CreateAsync(CreateDayVM vm);
        Task UpdateAsync(UpdateDayVM vm);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
