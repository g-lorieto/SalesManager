using SalesManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManager.Core.Interfaces
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(int id) where T : BaseEntity;
        Task<List<T>> ListAsync<T>() where T : BaseEntity;
        Task<int> AddAsync<T>(T entity) where T : BaseEntity;
        Task<int> UpdateAsync<T>(T entity) where T : BaseEntity;
        Task<int> DeleteAsync<T>(int id) where T : BaseEntity;
        Task<bool> EntityExistsAsync<T>(int id) where T : BaseEntity;
    }
}
