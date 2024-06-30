using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.DayVMs;
using TREKER.Core.Entities;
using TREKER.DAL.Repositories.Implementations;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.Business.Services.Abstractions
{
    public class DayService : IDayService
    {
        private readonly IDayRepository _dayRepository;
        private readonly string[] includes =
        {
            "Trekking"
        };

        public DayService(IDayRepository dayRepository)
        {
            _dayRepository = dayRepository;
        }

        public async Task CreateAsync(CreateDayVM vm)
        {
            var newDay = new Day()
            {
                Description = vm.Description,
                TrekkingId = vm.TrekkingId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _dayRepository.CreateAsync(newDay);
            await _dayRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _dayRepository.DeleteAsync(id);
            await _dayRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<Day>> GetAllAsync()
        {
            return await _dayRepository.GetAllAsync(includes: includes);
        }

        public async Task<Day> GetByIdAsync(int id)
        {
            return await _dayRepository.GetByIdAsync(id, includes);
        }

        public async Task RecoverAsync(int id)
        {
            await _dayRepository.RecoverAsync(id);
            await _dayRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _dayRepository.Remove(id);
            await _dayRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateDayVM vm)
        {
            var oldDay = await _dayRepository.GetByIdAsync(vm.Id);

            oldDay.Description = vm.Description ?? oldDay.Description;
            oldDay.TrekkingId = vm.TrekkingId;
            oldDay.UpdatedDate = DateTime.UtcNow;

            await _dayRepository.UpdateAsync(oldDay);
            await _dayRepository.SaveChangesAsync();
        }
    }
}
