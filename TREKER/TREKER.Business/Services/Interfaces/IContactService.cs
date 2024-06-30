using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.BlogVMs;
using TREKER.Business.ViewModels.ContactVMs;
using TREKER.Business.ViewModels.RegionVMs;
using TREKER.Core.Entities;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.Business.Services.Interfaces
{
    public interface IContactService 
    {
        Task<IQueryable<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int id);
        Task CreateAsync(ContactVM vm);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
