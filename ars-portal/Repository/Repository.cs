using ars_portal.Models.Models;
using ars_portal.Models.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ars_portal.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        
        private readonly DbContext context;

        private readonly DbSet<T> entities;

       
        public Repository(DbContext _context)
        {
            this.context = _context;
            entities = context.Set<T>();
        }

       
        public async Task<T> GetItemAsync(int id)
        {            
            var result = await entities.SingleOrDefaultAsync<T>(e => e.Id == id);
            return result;
        }

        public async Task<T> GetItemAsync(Expression<Func<T, bool>> predicate)
        {
            return await entities.SingleOrDefaultAsync<T>(predicate);
        }

        public async Task<IQueryable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> results = await Task.Run(() => entities.Where(predicate));
            return results;
        }
        public async Task<bool> CreateItemsAsync(List<T> items)
        {
            foreach (var item in items)
            {
                await CreateItemAsync(item);
            }
            return true;
        }

        public async Task<T> CreateItemAsync(T item)
        {
            
            entities.Add(item);
            await context.SaveChangesAsync();          
            return item;

        }

        public async Task<T> DeleteItemAsync(int id)
        {          
            Task<IQueryable<T>> results = GetItemsAsync(T => T.Id == id);
            var item = results.Result.FirstOrDefault();
            if (item != null)
              {
                context.Remove(item);
                await context.SaveChangesAsync();
              }
            return item;           
        }

        public async Task<T> UpdateItemAsync(string id, T item)
        {
            Task<IQueryable<T>> results = GetItemsAsync(T => T.Id.ToString() == id);
            T oldItem = await results.Result.FirstOrDefaultAsync();
            context.Entry<T>(oldItem).State = EntityState.Detached;
            entities.Update(item);
            await context.SaveChangesAsync();
            results = GetItemsAsync(T => T.Id.ToString() == id);
            return await results.Result.FirstOrDefaultAsync();
        }     
    }
}
