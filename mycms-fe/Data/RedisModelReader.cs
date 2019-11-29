using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using mycms.Data.Infrastructure;
using StackExchange.Redis;

namespace mycms_fe.Data
{
    public class RedisModelReader<T> : IModelReader<T> where T : class
    {
        private readonly IEnumerable<T> collection = null;

        public RedisModelReader(
            IDatabase cache)
        {
            var json = cache.StringGet(typeof(T).Name);
            this.collection = string.IsNullOrEmpty(json)
                ? new List<T>() 
                : JsonSerializer.Deserialize<IEnumerable<T>>(json);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType => this.collection.AsQueryable().ElementType;

        public Expression Expression => this.collection.AsQueryable().Expression;

        public IQueryProvider Provider => this.collection.AsQueryable().Provider;

        public void Dispose() { }
    }
}