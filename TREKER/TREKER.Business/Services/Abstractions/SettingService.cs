using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.PageVM;
using TREKER.Core.Entities;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.Business.Services.Abstractions
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;


        public async Task Update(LayoutVM vm)
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
            oldSetting.LogoUrl = vm.LogoUrl ?? oldSetting.LogoUrl;

            await _settingRepository.UpdateAsync(oldSetting);
            await _settingRepository.SaveChangesAsync();
        }

        async Task<Setting> ISettingService.GetByIdAsync(int id)
        {
            return await _settingRepository.GetByIdAsync(id);
        }
    }
}
