using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.ContactVMs;
using TREKER.Core.Entities;
using TREKER.DAL.Repositories.Implementations;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.Business.Services.Abstractions
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task CreateAsync(ContactVM vm)
        {
            var newContact = new Contact()
            {
                Name = vm.Name,
                Email = vm.Email,
                Phone = vm.Phone,
                Subject = vm.Subject,
                Message = vm.Message,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _contactRepository.CreateAsync(newContact);
            await _contactRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _contactRepository.DeleteAsync(id);
            await _contactRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<Contact>> GetAllAsync()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            return await _contactRepository.GetByIdAsync(id);
        }

        public async Task RecoverAsync(int id)
        {
            await _contactRepository.RecoverAsync(id);
            await _contactRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _contactRepository.Remove(id);
            await _contactRepository.SaveChangesAsync();
        }
    }
}
