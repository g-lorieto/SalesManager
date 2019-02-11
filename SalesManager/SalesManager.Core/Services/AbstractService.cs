using SalesManager.Core.Interfaces;
using SalesManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalesManager.Core.Services
{
    public abstract class AbstractService<T> where T : BaseEntity
    {
        private IRepository _repository;

        public AbstractService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<T> AddAsync(T entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync<T>(id);
        }

        public async Task<List<T>> ListAsync()
        {
            return await _repository.ListAsync<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync<T>(id);
        }

        public async Task UpdateAsync(int id, T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task<bool> EntityExistsAsync(int id)
        {
            return await _repository.EntityExistsAsync<T>(id);
        }

        public abstract bool ValidateEntity(T entity);
    }
}
