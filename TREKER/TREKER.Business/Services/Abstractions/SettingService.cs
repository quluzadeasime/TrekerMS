using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Helpers;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.PageVM;
using TREKER.Core.Entities;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.Business.Services.Abstractions
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public SettingService(ISettingRepository settingRepository, IConfiguration configuration)
        {
            _settingRepository = settingRepository;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("AzureContainer");
        }

        public async Task<IQueryable<Setting>> GetAllAsync()
        {
            return await _settingRepository.GetAllAsync();
        }

        public async Task UpdateAsync(SettingVM vm)
        {
            var oldSetting = await _settingRepository.GetByIdAsync(vm.Id);

            oldSetting.Instagram = vm.Instagram ?? oldSetting.Instagram;
            oldSetting.Address1 = vm.Address1 ?? oldSetting.Address1;
            oldSetting.Address2 = vm.Address2 ?? oldSetting.Address2;
            oldSetting.Phone1 = vm.Phone1 ?? oldSetting.Phone1;
            oldSetting.Phone2 = vm.Phone2 ?? oldSetting.Phone2;
            oldSetting.Facebook = vm.Facebook ?? oldSetting.Facebook;
            oldSetting.Twitter = vm.Twitter ?? oldSetting.Twitter;
            oldSetting.Youtube = vm.Youtube ?? oldSetting.Youtube;
            oldSetting.Description = vm.Description ?? oldSetting.Description;
            oldSetting.Email = vm.Email ?? oldSetting.Email;

            if (vm.File is not null)
            {
                if (!string.IsNullOrEmpty(oldSetting.LogoUrl) && vm.File != null)
                {
                    Uri uri = new Uri(oldSetting.LogoUrl);
                    string blobName = uri.Segments.Last();
                    await FileManager.DeleteFileAsync(blobName, _connectionString, "SettingsPictures/");
                }

                oldSetting.LogoUrl = await vm.File.UploadFileAsync(_connectionString, "SettingsPictures/");
            }

            await _settingRepository.UpdateAsync(oldSetting);
            await _settingRepository.SaveChangesAsync();
        }
    }
}
