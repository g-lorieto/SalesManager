using SalesManager.Core.Interfaces;
using SalesManager.Core.Models;
using SalesManager.Core.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public async Task<Result<int>> AddAsync(T entity)
        {
            try
            {
                return Ok(await _repository.AddAsync(entity));
            }
            catch (Exception ex)
            {
                return Failure(-1, ex.Message);
            }
        }
        public async Task<Result<int>> DeleteAsync(int id)
        {
            try
            {
                return Ok(await _repository.DeleteAsync<T>(id));
            }
            catch (Exception ex)
            {
                return Failure(-1, ex.Message);
            }
        }

        public async Task<Result<List<T>>> ListAsync()
        {
            try
            {
                return Ok(await _repository.ListAsync<T>());
            }
            catch (Exception ex)
            {
                return Failure((List<T>) null, ex.Message);
            }
        }

        public async Task<Result<T>> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                return Ok(await _repository.GetByIdAsync<T>(id, includes));
            }
            catch (Exception ex)
            {
                return Failure((T) null, ex.Message);
            }
        }

        public async Task<Result<int>> UpdateAsync(int id, T entity, params Expression<Func<T, object>>[] navigations)
        {
            try
            {
                return Ok(await _repository.UpdateAsync(entity, navigations));
            }
            catch (Exception ex)
            {
                return Failure(-1, ex.Message);
            }
        }

        public async Task<Result<bool>> EntityExistsAsync(int id)
        {
            try
            { 
                return Ok(await _repository.EntityExistsAsync<T>(id));
            }
            catch (Exception ex)
            {
                return Failure(false, ex.Message);
            }
        }

        public abstract Result<bool> ValidateEntity(T entity);

        public Result<U> Ok<U>(U data, string message = "")
        {
            return new Result<U>(data, true, message);
        }

        public Result<U> Failure<U>(U data, string message = "")
        {
            return new Result<U>(data, false, message); 
        }
    }
}
