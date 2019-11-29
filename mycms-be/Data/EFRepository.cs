using System;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using mycms.Data.Infrastructure;
using StackExchange.Redis;

namespace mycms.Data
{
    public class EFRepository<T, K> 
        : IDisposable, IRepository<T, K> where T: class
    {
        private readonly MyCmsDbContext context;
        private readonly IDatabase cache;
        private readonly DbSet<T> entitySet;

        public EFRepository(
            MyCmsDbContext context,
            IDatabase cache)
        {
            this.context = context;
            this.cache = cache;
            this.entitySet = this.context.Set<T>();
        }
        
        public IQueryable<T> GetAll()
        {
            return this.entitySet.AsNoTracking<T>().AsQueryable();
        }

        public T Get(K key)
        {
            return this.context.Find<T>(key);
        }

        public void Create(T entity)
        {
            this.entitySet.Add(entity);
        }

        public void Update(T entity)
        {
            this.entitySet.Update(entity);
        }

        public void CreateOrUpdate(T entity, K key)
        {
            T result = this.entitySet.Find(key);
            if(result == null) this.entitySet.Add(entity);
            else context.Entry(result).CurrentValues.SetValues(entity);
        }

        public void Delete(T entity)
        {
            this.entitySet.Remove(entity);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
            var json = JsonSerializer.Serialize(this.GetAll().ToList());
            this.cache.StringSet(typeof(T).Name, json);
        }

        public void Dispose()
        {
            if (this.context != null)
                this.context.Dispose();
        }
    }
}
