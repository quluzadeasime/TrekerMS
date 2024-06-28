using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.TrekkingVMs;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Abstractions
{
    public class TrekkingService : ITrekkingService
    {
        public Task CreateAsync(CreateTrekkingVM vm)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Trekking>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Trekking> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RecoverAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UpdateTrekkingVM vm)
        {
            throw new NotImplementedException();
        }
    }
}
