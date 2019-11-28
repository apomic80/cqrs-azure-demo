using System;
using System.Linq;

namespace mycms.Data.Infrastructure
{
    public interface IRepository<T, K> : IDisposable
        where T : class
    {
        IQueryable<T> GetAll();
        T Get(K key);
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
        void CreateOrUpdate(T entity, K key);
        void SaveChanges();
    }
}