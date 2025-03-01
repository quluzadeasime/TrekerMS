﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.PageVM;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Interfaces
{
    public interface ISettingService
    {
        Task<IQueryable<Setting>> GetAllAsync();
        Task UpdateAsync(SettingVM vm);
    }
}
