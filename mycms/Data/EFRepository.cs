using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using mycms.Data.Infrastructure;

namespace mycms.Data
{
    public class EFRepository<T, K> 
        : IDisposable, IRepository<T, K> where T: class
    {
        private readonly MyCmsDbContext context;
        private readonly DbSet<T> entitySet;

        public EFRepository(MyCmsDbContext context)
        {
            this.context = context;
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
        }

        public void Dispose()
        {
            if (this.context != null)
                this.context.Dispose();
        }
    }
}
