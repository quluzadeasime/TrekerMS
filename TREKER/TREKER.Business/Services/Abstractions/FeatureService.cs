using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.FeatureVMs;
using TREKER.Core.Entities;
using TREKER.DAL.Repositories.Implementations;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.Business.Services.Abstractions
{
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;

        public FeatureService(IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }

        public async Task CreateAsync(CreateFeatureVM vm)
        {
            var newFeature = new Feature()
            {
                Name = vm.Name,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _featureRepository.CreateAsync(newFeature);
            await _featureRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _featureRepository.DeleteAsync(id);
            await _featureRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<Feature>> GetAllAsync()
        {
            return await _featureRepository.GetAllAsync();
        }

        public async Task<Feature> GetByIdAsync(int id)
        {
            return await _featureRepository.GetByIdAsync(id);
        }

        public async Task RecoverAsync(int id)
        {
            await _featureRepository.RecoverAsync(id);
            await _featureRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _featureRepository.Remove(id);
            await _featureRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateFeatureVM vm)
        {
            var oldFeature = await _featureRepository.GetByIdAsync(vm.Id);

            oldFeature.Name = vm.Name ?? oldFeature.Name;
            oldFeature.UpdatedDate = DateTime.UtcNow;

            await _featureRepository.UpdateAsync(oldFeature);
            await _featureRepository.SaveChangesAsync();
        }
    }
}
