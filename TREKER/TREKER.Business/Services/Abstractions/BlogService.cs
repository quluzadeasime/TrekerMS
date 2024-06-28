using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.BlogVMs;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Abstractions
{
    public class BlogService : IBlogService
    {
        public Task CreateAsync(CreateBlogVM vm)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Blog>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Blog> GetByIdAsync(int id)
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

        public Task UpdateAsync(UpdateBlogVM vm)
        {
            throw new NotImplementedException();
        }
    }
}
