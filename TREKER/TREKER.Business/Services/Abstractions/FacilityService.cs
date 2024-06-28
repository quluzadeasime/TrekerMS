using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.FacilityVMs;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Abstractions
{
    public class FacilityService : IFacilityService
    {
        public Task CreateAsync(CreateFacilityVM vm)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Facility>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Facility> GetByIdAsync(int id)
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

        public Task UpdateAsync(UpdateFacilityVM vm)
        {
            throw new NotImplementedException();
        }
    }
}
