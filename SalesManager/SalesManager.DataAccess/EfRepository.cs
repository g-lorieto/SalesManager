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
    public class EfRepository : IRepository
    {
        private readonly SalesManagerContext _dbContext;

        public EfRepository(SalesManagerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync<T>(T entity) where T : BaseEntity
        {
            try
            {
                _dbContext.Set<T>().Add(entity);
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> DeleteAsync<T>(int id) where T : BaseEntity
        {
            var entity = await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
            _dbContext.Set<T>().Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> EntityExistsAsync<T>(int id) where T : BaseEntity
        {
            return await _dbContext.Set<T>().AnyAsync(e => e.Id == id);
        }

        public async Task<T> GetByIdAsync<T>(int id, params Expression<Func<T, object>>[] includes) where T : BaseEntity
        {
            var query = _dbContext.Set<T>().AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query,
                  (current, include) => current.Include(include));
            }

            return await query.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<T>> ListAsync<T>() where T : BaseEntity
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<int> UpdateAsync<T>(T entity, params Expression<Func<T, object>>[] navigations) where T : BaseEntity
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
