using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ars_portal.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetItemAsync(int id);
        Task<T> GetItemAsync(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate);
        Task<bool> CreateItemsAsync(List<T> items);
        Task<T> CreateItemAsync(T item);
        Task<T> DeleteItemAsync(int id);
        Task<T> UpdateItemAsync(string id, T item);


    }
}
