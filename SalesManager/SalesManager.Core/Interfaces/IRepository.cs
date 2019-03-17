using SalesManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalesManager.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<List<T>> ListAsync();
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity, params Expression<Func<T, object>>[] navigations);
        Task<int> DeleteAsync(int id);
        Task<bool> EntityExistsAsync(int id);
        IQueryable<T> Query();
    }
}
