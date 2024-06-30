using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Helpers;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.DestinationVMs;
using TREKER.Core.Entities;
using TREKER.DAL.Repositories.Implementations;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.Business.Services.Abstractions
{
    public class DestinationService : IDestinationService
    {
        private readonly IDestinationRepository _destinationRepository;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string[] includes =
        {
            "Region"
        };

        public DestinationService(IDestinationRepository destinationRepository, IConfiguration configuration)
        {
            _destinationRepository = destinationRepository;
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("AzureContainer");
        }

        public async Task CreateAsync(CreateDestinationVM vm)
        {
            var newDestination = new Destination()
            {
                Title = vm.Title,
                RegionId = vm.RegionId,
                ImageUrl = await vm.File.UploadFileAsync(_connectionString, "/DestinationPictures/"),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };

            await _destinationRepository.CreateAsync(newDestination);
            await _destinationRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _destinationRepository.DeleteAsync(id);
            await _destinationRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<Destination>> GetAllAsync()
        {
            return await _destinationRepository.GetAllAsync(includes: includes);
        }

        public async Task<Destination> GetByIdAsync(int id)
        {
            return await _destinationRepository.GetByIdAsync(id,includes);
        }

        public async Task RecoverAsync(int id)
        {
            await _destinationRepository.RecoverAsync(id);
            await _destinationRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var oldDestination = await _destinationRepository.GetByIdAsync(id,includes);

            Uri uri = new Uri(oldDestination.ImageUrl);
            string blobName = uri.Segments.Last();
            await FileManager.DeleteFileAsync(blobName, _connectionString, "/DestinationPictures/");

            _destinationRepository.Remove(id);
            await _destinationRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateDestinationVM vm)
        {
            var oldDestination = await _destinationRepository.GetByIdAsync(vm.Id,includes);

            oldDestination.Title = vm.Title ?? oldDestination.Title;
            oldDestination.RegionId = vm.RegionId ?? oldDestination.RegionId;
            oldDestination.UpdatedDate = DateTime.UtcNow;

            if (vm.File is not null)
            {
                if (!string.IsNullOrEmpty(oldDestination.ImageUrl) && vm.File != null)
                {
                    Uri uri = new Uri(oldDestination.ImageUrl);
                    string blobName = uri.Segments.Last();
                    await FileManager.DeleteFileAsync(blobName, _connectionString, "/DestinationPictures/");
                }

                oldDestination.ImageUrl = await vm.File.UploadFileAsync(_connectionString, "/DestinationPictures/");
            }

            await _destinationRepository.UpdateAsync(oldDestination);
            await _destinationRepository.SaveChangesAsync();
        }
    }
}
