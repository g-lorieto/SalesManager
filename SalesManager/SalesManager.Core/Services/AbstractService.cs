using SalesManager.Core.Interfaces;
using SalesManager.Core.Models;
using SalesManager.Core.Models.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalesManager.Core.Services
{
    public abstract class AbstractService<T> : Result<U> where T : BaseEntity U
    {
        private IRepository _repository;

        public AbstractService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> AddAsync(T entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            await _repository.DeleteAsync<T>(id);
        }

        public async Task<List<Result>> ListAsync()
        {
            return await _repository.ListAsync<T>();
        }

        public async Task<Result> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync<T>(id);
        }

        public async Task<Result> UpdateAsync(int id, T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task<Result> EntityExistsAsync(int id)
        {
            return await _repository.EntityExistsAsync<T>(id);
        }

        public abstract Result ValidateEntity(T entity);
    }
}
