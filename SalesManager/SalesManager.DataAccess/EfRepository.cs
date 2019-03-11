using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
            var dbEntity = _dbContext.Set<T>().Find(entity.Id);

            var dbEntry = _dbContext.Entry(dbEntity);

            dbEntry.CurrentValues.SetValues(entity);

            dbEntry.State = EntityState.Modified;

            foreach (var property in navigations)
            {
                var propertyName = property.GetPropertyAccess().Name;

                await dbEntry.Collection(propertyName).LoadAsync();

                List<BaseEntity> dbChilds = dbEntry.Collection(propertyName).CurrentValue.Cast<BaseEntity>().ToList();

                foreach (BaseEntity child in dbChilds)
                {
                    if (child.Id == 0)
                    {
                        _dbContext.Entry(child).State = EntityState.Added;
                    }

                    if (deletedEntities.Contains(child.Id))
                    {
                        _dbContext.Entry(child).State = EntityState.Deleted;
                    }
                    else
                    {
                        _dbContext.Entry(child).State = EntityState.Modified;
                    }
                }
            }

            return await _dbContext.SaveChangesAsync();
        }
    }
}
