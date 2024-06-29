using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.FacilityVMs;
using TREKER.Core.Entities;
using TREKER.DAL.Repositories.Implementations;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.Business.Services.Abstractions
{
    public class FacilityService : IFacilityService
    {
        private readonly IFacilityRepository _facilityRepository;

        public FacilityService(IFacilityRepository facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }

        public async Task CreateAsync(CreateFacilityVM vm)
        {
            var newFacility = new Facility()
            {
                Name = vm.Name,
                Icon = vm.Icon,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _facilityRepository.CreateAsync(newFacility);
            await _facilityRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _facilityRepository.DeleteAsync(id);
            await _facilityRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<Facility>> GetAllAsync()
        {
            return await _facilityRepository.GetAllAsync();
        }

        public async Task<Facility> GetByIdAsync(int id)
        {
            return await _facilityRepository.GetByIdAsync(id);
        }

        public async Task RecoverAsync(int id)
        {
            await _facilityRepository.RecoverAsync(id);
            await _facilityRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _facilityRepository.Remove(id);
            await _facilityRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateFacilityVM vm)
        {
            var oldFacility = await _facilityRepository.GetByIdAsync(vm.Id);

            oldFacility.Name = vm.Name ?? oldFacility.Name;
            oldFacility.Icon = vm.Icon ?? oldFacility.Icon;
            oldFacility.UpdatedDate = DateTime.UtcNow;

            await _facilityRepository.UpdateAsync(oldFacility);
            await _facilityRepository.SaveChangesAsync();
        }
    }
}
