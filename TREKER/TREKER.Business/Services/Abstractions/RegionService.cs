using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.RegionVMs;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Abstractions
{
    public class RegionService : IRegionService
    {
        public Task CreateAsync(CreateRegionVM vm)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Region>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Region> GetByIdAsync(int id)
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

        public Task UpdateAsync(UpdateRegionVM vm)
        {
            throw new NotImplementedException();
        }
    }
}
