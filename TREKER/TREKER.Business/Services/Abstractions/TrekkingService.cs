using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Helpers;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.TrekkingVMs;
using TREKER.Core.Entities;
using TREKER.DAL.Repositories.Implementations;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.Business.Services.Abstractions
{
    public class TrekkingService : ITrekkingService
    {
        private readonly ITrekkingRepository _trekkingRepository;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string[] includes =
        {
            "Difficulty",
            "Destination",
            "Days",
            "Features",
            "Features.Feature",
            "Facilities",
            "Facilities.Facility"
        };

        public TrekkingService(ITrekkingRepository trekkingRepository, IConfiguration configuration)
        {
            _trekkingRepository = trekkingRepository;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("AzureContainer");
        }

        public async Task CreateAsync(CreateTrekkingVM vm)
        {
            var newTrekking = new Trekking
            {
                Title = vm.Title,
                SubTitle = vm.SubTitle,
                Price = vm.Price,
                Description = vm.Description,
                Star = vm.Star,
                ReviewCount = vm.ReviewCount,
                Duration = vm.Duration,
                GroupSize = vm.GroupSize,
                RoadHeight = vm.RoadHeight,
                DestinationId = vm.DestinationId,
                DifficultyId = vm.DifficultyId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };

            CreateTrekkingFeatures(vm, ref newTrekking);
            CreateTrekkingFacilities(vm, ref newTrekking);

            newTrekking.Images = (await CreateTrekkingImages(vm, newTrekking)).AsQueryable();

            await _trekkingRepository.CreateAsync(newTrekking);
            await _trekkingRepository.SaveChangesAsync();
        }

        public void CreateTrekkingFeatures(CreateTrekkingVM vm, ref Trekking newTrekking)
        {
            foreach (var featureId in vm.FeatureIds)
            {
                newTrekking.Features.ToList().Add(
                    new TrekkingFeature
                    {
                        Trekking = newTrekking,
                        FeatureId = featureId,
                    }
                    );
            }            
        }

        public void CreateTrekkingFacilities(CreateTrekkingVM vm, ref Trekking newTrekking)
        {
            foreach (var facilityId in vm.FacilityIds)
            {
                newTrekking.Facilities.ToList().Add(
                    new TrekkingFacility
                    {
                        Trekking = newTrekking,
                        FacilityId = facilityId,
                    }
                    );
            }
        }

        public async Task<List<TrekkingImage>> CreateTrekkingImages(CreateTrekkingVM vm, Trekking newTrekking)
        {
            List<TrekkingImage> trekkingImages = new List<TrekkingImage>();

            foreach (var image in vm.Images)
            {
                trekkingImages.Add(
                    new TrekkingImage
                    {
                        Trekking = newTrekking,
                        ImageUrl = await image.UploadFileAsync(_connectionString, "/TrekkingPictures/")
                    }
                    );
            }

            return trekkingImages;
        }

        public async Task DeleteAsync(int id)
        {
            await _trekkingRepository.DeleteAsync(id);
            await _trekkingRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<Trekking>> GetAllAsync()
        {
            return await _trekkingRepository.GetAllAsync(includes: includes);
        }

        public async Task<Trekking> GetByIdAsync(int id)
        {
            return await _trekkingRepository.GetByIdAsync(id, includes);
        }

        public async Task RecoverAsync(int id)
        {
            await _trekkingRepository.RecoverAsync(id);
            await _trekkingRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var oldTrekking = await _trekkingRepository.GetByIdAsync(id, includes);

            foreach (var item in oldTrekking.Images)
            {
                oldTrekking.Images.ToList().Remove(item);

                Uri uri = new Uri(item.ImageUrl);
                string blobName = uri.Segments.Last();
                await FileManager.DeleteFileAsync(blobName, _connectionString, "/TrekkingPictures/");
            }

            _trekkingRepository.Remove(id);
            await _trekkingRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateTrekkingVM vm)
        {
            var oldTrekking = await _trekkingRepository.GetByIdAsync(vm.Id, includes);

            oldTrekking.Title = vm.Title ?? oldTrekking.Title;
            oldTrekking.Description = vm.Description ?? oldTrekking.Description;
            oldTrekking.SubTitle = vm.SubTitle ?? oldTrekking.SubTitle;
            oldTrekking.Price = vm.Price ?? oldTrekking.Price;
            oldTrekking.GroupSize = vm.GroupSize ?? oldTrekking.GroupSize;  
            oldTrekking.UpdatedDate = DateTime.UtcNow;
            oldTrekking.Star = vm.Star ?? oldTrekking.Star;
            oldTrekking.ReviewCount = vm.ReviewCount ?? oldTrekking.ReviewCount;
            oldTrekking.Duration = vm.Duration ?? oldTrekking.Duration;
            oldTrekking.RoadHeight = vm.RoadHeight ?? oldTrekking.RoadHeight;
            oldTrekking.DestinationId = vm.DestinationId ?? oldTrekking.DestinationId;
            oldTrekking.DifficultyId = vm.DifficultyId ?? oldTrekking.DifficultyId;

            UpdateTrekkingFacilities(vm, ref oldTrekking);
            UpdateTrekkingFeatures(vm, ref oldTrekking);

            oldTrekking.Images = (await UpdateTrekkingImagesAsync(vm, oldTrekking)).AsQueryable();

            await _trekkingRepository.CreateAsync(oldTrekking);
            await _trekkingRepository.SaveChangesAsync();


        }

        public void UpdateTrekkingFeatures(UpdateTrekkingVM vm, ref Trekking oldTrekking)
        {
            oldTrekking.Features.ToList().Clear();

            foreach (var featureId in vm.FeatureIds)
            {
                oldTrekking.Features.ToList().Add(
                    new TrekkingFeature
                    {
                        Trekking = oldTrekking,
                        FeatureId = featureId,
                    }
                    );
            }
        }

        public void UpdateTrekkingFacilities(UpdateTrekkingVM vm, ref Trekking oldTrekking)
        {
            oldTrekking.Facilities.ToList().Clear();

            foreach (var facilityId in vm.FacilityIds)
            {
                oldTrekking.Facilities.ToList().Add(
                    new TrekkingFacility
                    {
                        Trekking = oldTrekking,
                        FacilityId = facilityId,
                    }
                    );
            }
        }

        public async Task<List<TrekkingImage>> UpdateTrekkingImagesAsync(UpdateTrekkingVM vm, Trekking oldTrekking)
        {
            List<TrekkingImage> trekkingImages = oldTrekking.Images.ToList();

            if (vm.ViewImageIds == null)
            {
                trekkingImages.RemoveAll(x => x.TrekkingId == vm.Id);
            }
            else
            {
                List<TrekkingImage> removeList = trekkingImages.Where(pt => !vm.ViewImageIds.Contains(pt.Id) && pt.TrekkingId == vm.Id).ToList();

                if (removeList.Count > 0)
                {
                    foreach (var item in removeList)
                    {
                        oldTrekking.Images.ToList().Remove(item);

                        Uri uri = new Uri(item.ImageUrl);
                        string blobName = uri.Segments.Last();
                        await FileManager.DeleteFileAsync(blobName, _connectionString, "/TrekkingPictures/");
                    }
                }
            }

            foreach (var image in vm.Images)
            {
                trekkingImages.Add(
                    new TrekkingImage
                    {
                        ImageUrl = await image.UploadFileAsync(_connectionString, "/TrekkingPictures/"),
                        Trekking = oldTrekking,
                    }
                    );
            }

            return trekkingImages;
        }
    }
}
