using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.FeatureVMs;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Abstractions
{
    public class FeatureService : IFeatureService
    {
        public Task CreateAsync(CreateFeatureVM vm)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Feature>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Feature> GetByIdAsync(int id)
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

        public Task UpdateAsync(UpdateFeatureVM vm)
        {
            throw new NotImplementedException();
        }
    }
}
