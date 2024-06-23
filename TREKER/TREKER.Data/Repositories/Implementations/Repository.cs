using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities.Commons;
using TREKER.DAL.Context;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.DAL.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseAuditableEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;
        public Repository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _table = _context.Set<T>();
        }

        public async Task<T> CheckEntity(int id,
             params string[] entityIncludes
            )
        {
            IQueryable<T> query = await GetAllAsync();
            if (entityIncludes is not null)
            {
                for (int i = 0; i < entityIncludes.Length; i++)
                {
                    query = query.Include(entityIncludes[i]);
                }
            }

            var entity = await query.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _table.AddAsync(entity);
            return entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            entity.IsDeleted = true;
            await UpdateAsync(entity);

            return entity;
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T,
            bool>>? expression = null,
            Expression<Func<T, object>>? expressionOrder = null,
            bool isDescending = false,
            params string[] includes
            )
        {
            IQueryable<T> query = _table;

            if (expression is not null)
            {
                query = query.Where(expression);
            }
            if (expressionOrder is not null)
            {
                query = isDescending ? query.OrderByDescending(expressionOrder) : query.OrderBy(expressionOrder);
            }
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return query;
        }

        public async Task<T> GetByIdAsync(int id, params string[] entityIncludes)
        {
            return await CheckEntity(id, entityIncludes);
        }

        public async Task<T> RecoverAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            entity.IsDeleted = false;
            await UpdateAsync(entity);

            return entity;
        }

        public void Remove(int id, params string[] entityIncludes)
        {
            var entity = GetByIdAsync(id, entityIncludes).Result;

            _table.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _table.Update(entity);
            return entity;
        }
    }
}
