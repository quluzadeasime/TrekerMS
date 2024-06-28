using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.DestinationVMs;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Abstractions
{
    public class DestinationService : IDestinationService
    {
        public Task CreateAsync(CreateDestinationVM vm)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Destination>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Destination> GetByIdAsync(int id)
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

        public Task UpdateAsync(UpdateDestinationVM vm)
        {
            throw new NotImplementedException();
        }
    }
}
