
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
        private readonly ITrekkingFacilityRepository _trekkingFacilityRepository;
        private readonly ITrekkingFeatureRepository _trekkingFeatureRepository;
        private readonly IConfiguration _configuration;
        private readonly ITrekkingImageRepository _trekkingImageRepository;
        private readonly string _connectionString;
        private readonly string[] includes =
        {
            "Images",
            "Difficulty",
            "Destination",
            "Days",
            "Features",
            "Features.Feature",
            "Facilities",
            "Facilities.Facility"
        };

        private readonly string[] includes2 =
        {
            "Images",
            "Features",
            "Features.Feature",
            "Facilities",
            "Facilities.Facility"
        };


        public TrekkingService(ITrekkingRepository trekkingRepository, IConfiguration configuration, ITrekkingImageRepository trekkingImageRepository, ITrekkingFacilityRepository trekkingFacilityRepository, ITrekkingFeatureRepository trekkingFeatureRepository)
        {
            _trekkingRepository = trekkingRepository;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("AzureContainer");
            _trekkingImageRepository = trekkingImageRepository;
            _trekkingFacilityRepository = trekkingFacilityRepository;
            _trekkingFeatureRepository = trekkingFeatureRepository;
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
                Features = new List<TrekkingFeature>(),
                Facilities = new List<TrekkingFacility>(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };

            foreach (var featureId in vm.FeatureIds)
            {
                newTrekking.Features.Add(
                    new TrekkingFeature
                    {
                        Trekking = newTrekking,
                        FeatureId = featureId,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    }
                    );
            }

            foreach (var facilityId in vm.FacilityIds)
            {
                newTrekking.Facilities.Add(
                    new TrekkingFacility
                    {
                        Trekking = newTrekking,
                        FacilityId = facilityId,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow, 
                    }
                    );
            }

            newTrekking.Images = await CreateTrekkingImages(vm, newTrekking);

            await _trekkingRepository.CreateAsync(newTrekking);
            await _trekkingRepository.SaveChangesAsync();
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
                        ImageUrl = await image.UploadFileAsync(_connectionString, "/TrekkingPictures/"),
                        CreatedDate = DateTime.UtcNow, 
                        UpdatedDate = DateTime.UtcNow,
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
            var oldTrekking = await _trekkingRepository.GetByIdAsync(vm.Id, includes2);

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
            oldTrekking.DestinationId = vm.DestinationId;
            oldTrekking.DifficultyId = vm.DifficultyId;


            foreach (var item in oldTrekking.Features)
            {
                _trekkingFeatureRepository.Remove(item.Id);
            }

            foreach (var item in oldTrekking.Facilities)
            {
                _trekkingFacilityRepository.Remove(item.Id);
            }

            oldTrekking.Facilities.Clear();
            oldTrekking.Features.Clear();


            foreach (var featureId in vm.FeatureIds)
            {
                oldTrekking.Features.Add(
                    new TrekkingFeature
                    {
                        Trekking = oldTrekking,
                        FeatureId = featureId,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                    }
                    );
            }

            foreach (var facilityId in vm.FacilityIds)
            {
                oldTrekking.Facilities.Add(
                    new TrekkingFacility
                    {
                        Trekking = oldTrekking,
                        FacilityId = facilityId,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                    }
                    );
            }
            
            oldTrekking.Images = await UpdateTrekkingImagesAsync(vm, oldTrekking);

            await _trekkingRepository.UpdateAsync(oldTrekking);
            await _trekkingRepository.SaveChangesAsync();


        }

        public async Task<List<TrekkingImage>> UpdateTrekkingImagesAsync(UpdateTrekkingVM vm, Trekking oldTrekking)
        {
            List<TrekkingImage> trekkingImages = oldTrekking.Images.ToList();

            if (vm.ViewImageIds == null)
            {
                trekkingImages.RemoveAll(x => x.TrekkingId == vm.Id);

                foreach (var item in oldTrekking.Images)
                {
                    _trekkingImageRepository.Remove(item.Id);
                }
            }
            else
            {
                List<TrekkingImage> removeList = trekkingImages.Where(pt => !vm.ViewImageIds.Contains(pt.Id) && pt.TrekkingId == vm.Id).ToList();

                if (removeList.Count > 0)
                {
                    foreach (var item in removeList)
                    {
                        trekkingImages.Remove(item);

                        _trekkingImageRepository.Remove(item.Id);

                        Uri uri = new Uri(item.ImageUrl);
                        string blobName = uri.Segments.Last();
                        await FileManager.DeleteFileAsync(blobName, _connectionString, "/TrekkingPictures/");
                    }
                }
            }

            if(vm.Images is not null)
            {
                foreach (var image in vm.Images)
                {
                    trekkingImages.Add(
                        new TrekkingImage
                        {
                            ImageUrl = await image.UploadFileAsync(_connectionString, "/TrekkingPictures/"),
                            Trekking = oldTrekking,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedDate = DateTime.UtcNow,
                        }
                        );
                }
            }

            return trekkingImages;
        }
    }
}
