using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities.Commons;

namespace TREKER.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseAuditableEntity, new()
    {
        Task<IQueryable<T>> GetAllAsync(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? expressionOrder = null,
            bool isDescending = false,
            params string[] includes
            );
        Task<T> GetByIdAsync(int id, params string[] entityIncludes);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> CheckEntity(
            int id,
            params string[] entityIncludes
            );
        Task<T> DeleteAsync(int id);
        Task<T> RecoverAsync(int id);
        void Remove(int id, params string[] entityIncludes);
        Task<int> SaveChangesAsync();
    }
}
