using System;
using System.Linq;

namespace mycms.Data.Infrastructure
{
    public interface IModelReader<T> 
        : IDisposable, IQueryable<T> where T : class
    {
    }
}