using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.RegionVMs;
using TREKER.Core.Entities;
using TREKER.DAL.Repositories.Implementations;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.Business.Services.Abstractions
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;

        public RegionService(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task CreateAsync(CreateRegionVM vm)
        {
            var newRegion = new Region()
            {
                Name = vm.Name,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _regionRepository.CreateAsync(newRegion);
            await _regionRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _regionRepository.DeleteAsync(id);
            await _regionRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<Region>> GetAllAsync()
        {
            return await _regionRepository.GetAllAsync();
        }

        public async Task<Region> GetByIdAsync(int id)
        {
            return await _regionRepository.GetByIdAsync(id);
        }

        public async Task RecoverAsync(int id)
        {
            await _regionRepository.RecoverAsync(id);
            await _regionRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _regionRepository.Remove(id);
            await _regionRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateRegionVM vm)
        {
            var oldRegion = await _regionRepository.GetByIdAsync(vm.Id);

            oldRegion.Name = vm.Name ?? oldRegion.Name;
            oldRegion.UpdatedDate = DateTime.UtcNow;

            await _regionRepository.UpdateAsync(oldRegion);
            await _regionRepository.SaveChangesAsync();
        }
    }
}
