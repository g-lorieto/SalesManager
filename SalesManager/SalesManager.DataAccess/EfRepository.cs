using Microsoft.EntityFrameworkCore;
using SalesManager.Core.Interfaces;
using SalesManager.Core.Models;
using System;
using System.Collections.Generic;
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

        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync<T>(int id) where T : BaseEntity
        {
            var entity = await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> EntityExistsAsync<T>(int id) where T : BaseEntity
        {
            return await _dbContext.Set<T>().AnyAsync(e => e.Id == id);
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<T>> ListAsync<T>() where T : BaseEntity
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
