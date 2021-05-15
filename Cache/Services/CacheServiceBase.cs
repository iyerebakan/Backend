using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cache
{
    internal abstract class CacheServiceBase : ICacheService
    {
        public Task<T> GetAsync<T>(CancellationToken token = default)
            where T : class, new()
        {
            return this.GetAsync<T>(typeof(T).FullName, token);
        }

        public abstract Task<T> GetAsync<T>(string key, CancellationToken token = default)
            where T : class, new();

        public Task<(bool keyExists, T cacheItem)> TryGetAsync<T>(
            CancellationToken token = default)
            where T : class, new()
        {
            return this.TryGetAsync<T>(typeof(T).FullName, token);
        }

        public abstract Task<(bool keyExists, T cacheItem)> TryGetAsync<T>(
            string key,
            CancellationToken token = default)
            where T : class, new();

        public Task SetAsync<T>(T cacheItem, CancellationToken token = default)
            where T : class, new()
        {
            return this.SetAsync<T>(cacheItem, typeof(T).FullName, token);
        }

        public abstract Task SetAsync<T>(
            T cacheItem,
            string key,
            CancellationToken token = default)
            where T : class, new();

        public abstract Task SetAsync<T>(T cacheItem, string key, DistributedCacheEntryOptions options, CancellationToken token = default)
            where T : class, new();

        public Task<bool> ExistsAsync<T>(CancellationToken token = default)
            where T : class, new()
        {
            return this.ExistsAsync<T>(typeof(T).FullName, token);
        }

        public abstract Task<bool> ExistsAsync<T>(string key, CancellationToken token = default)
            where T : class, new();

        public Task RemoveAsync<T>(CancellationToken token = default)
            where T : class, new()
        {
            return this.RemoveAsync<T>(typeof(T).FullName, token);
        }

        public abstract Task RemoveAsync<T>(string key, CancellationToken token = default)
            where T : class, new();
    }
}
