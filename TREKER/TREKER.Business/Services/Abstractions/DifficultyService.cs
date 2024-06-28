using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.DifficultyVMs;
using TREKER.Core.Entities;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.Business.Services.Abstractions
{
    public class DifficultyService : IDifficultyService
    {
        private readonly IDifficultyRepository _difficultyRepository;
        
        public DifficultyService(IDifficultyRepository difficultyRepository)
        {
            _difficultyRepository = difficultyRepository;
        }

        public async Task CreateAsync(CreateDifficultyVM vm)
        {
            var newDifficulty = new Difficulty()
            {
                Name = vm.Name,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _difficultyRepository.CreateAsync(newDifficulty);
            await _difficultyRepository.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            await _difficultyRepository.DeleteAsync(id);
            await _difficultyRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<Difficulty>> GetAllAsync()
        {
            return await _difficultyRepository.GetAllAsync();
        }

        public async Task<Difficulty> GetByIdAsync(int id)
        {
            return await _difficultyRepository.GetByIdAsync(id);
        }

        public async Task RecoverAsync(int id)
        {
            await _difficultyRepository.RecoverAsync(id);
            await _difficultyRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _difficultyRepository.Remove(id);
            await _difficultyRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateDifficultyVM vm)
        {
            var oldDifficulty = await _difficultyRepository.GetByIdAsync(vm.Id);

            oldDifficulty.Name = vm.Name ?? oldDifficulty.Name;
            oldDifficulty.UpdatedDate = DateTime.UtcNow;

            await _difficultyRepository.UpdateAsync(oldDifficulty);
            await _difficultyRepository.SaveChangesAsync();
        }
    }
}
