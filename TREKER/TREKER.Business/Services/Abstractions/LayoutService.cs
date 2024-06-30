using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Core.Entities;
using TREKER.DAL.Context;

namespace TREKER.Business.Services.Abstractions
{
    public   class LayoutService
    {
        private readonly ISettingService _settingService;

        public LayoutService(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<Setting> GetSettingsAsync()
        {
            var settings = await (await _settingService.GetAllAsync()).FirstOrDefaultAsync();

            return settings;
        }
    }
}
