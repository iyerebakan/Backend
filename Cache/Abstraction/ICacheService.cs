using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cache
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(CancellationToken token = default)
             where T : class, new();

        Task<T> GetAsync<T>(string key, CancellationToken token = default)
            where T : class, new();

        Task<(bool keyExists, T cacheItem)> TryGetAsync<T>(CancellationToken token = default)
            where T : class, new();

        Task<(bool keyExists, T cacheItem)> TryGetAsync<T>(string key, CancellationToken token = default)
            where T : class, new();

        Task SetAsync<T>(T cacheItem, CancellationToken token = default)
            where T : class, new();

        Task SetAsync<T>(T cacheItem, string key, CancellationToken token = default)
            where T : class, new();
        Task SetAsync<T>(T cacheItem, string key, DistributedCacheEntryOptions options,
            CancellationToken token = default)
            where T : class, new();

        Task<bool> ExistsAsync<T>(CancellationToken token = default)
            where T : class, new();

        Task<bool> ExistsAsync<T>(string key, CancellationToken token = default)
            where T : class, new();

        Task RemoveAsync<T>(CancellationToken token = default)
            where T : class, new();

        Task RemoveAsync<T>(string key, CancellationToken token = default)
            where T : class, new();
    }
}
