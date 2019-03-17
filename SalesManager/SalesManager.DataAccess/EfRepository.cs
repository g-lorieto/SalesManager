using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SalesManager.Core.Interfaces;
using SalesManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalesManager.DataAccess
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SalesManagerContext _dbContext;
        private DbSet<T> _dbSet;

        public EfRepository(SalesManagerContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<int> AddAsync(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(e => e.Id == id);
            _dbSet.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> EntityExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query,
                  (current, include) => current.Include(include));
            }

            return await query.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<T>> ListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<int> UpdateAsync(T entity, params Expression<Func<T, object>>[] navigations)
        {
            try
            {
                var dbEntity = await _dbContext.FindAsync<T>(entity.Id);

                var dbEntry = _dbContext.Entry(dbEntity);
                dbEntry.CurrentValues.SetValues(entity);

                foreach (var property in navigations)
                {
                    var propertyName = property.GetPropertyAccess().Name;
                    var dbItemsEntry = dbEntry.Collection(propertyName);
                    var accessor = dbItemsEntry.Metadata.GetCollectionAccessor();

                    await dbItemsEntry.LoadAsync();
                    var dbItemsMap = ((IEnumerable<BaseEntity>)dbItemsEntry.CurrentValue)
                        .ToDictionary(e => e.Id);

                    var items = (IEnumerable<BaseEntity>)accessor.GetOrCreate(entity);

                    foreach (var item in items)
                    {
                        if (!dbItemsMap.TryGetValue(item.Id, out var oldItem))
                            accessor.Add(dbEntity, item);
                        else
                        {
                            _dbContext.Entry(oldItem).CurrentValues.SetValues(item);
                            dbItemsMap.Remove(item.Id);
                        }
                    }

                    foreach (var oldItem in dbItemsMap.Values)
                    {
                        accessor.Remove(dbEntity, oldItem);                        
                    }
                }

                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
