using Cache.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cache
{
    internal class MemoryCacheService : CacheServiceBase, IMemoryCacheService, IMemoryCache
    {
        private readonly IMemoryCache memoryCache;

        public MemoryCacheService(IOptions<MemoryCacheOptions> options)
        {
            this.memoryCache = new MemoryCache(options);
        }

        public override Task<T> GetAsync<T>(string key, CancellationToken token = default)
        {
            return Task.Run(
                () =>
                {
                    var cacheItem = this.memoryCache.Get<CacheItem<T>>(key);
                    if (cacheItem == null)
                    {
                        throw new Exception(key);
                    }

                    return cacheItem.Value;
                }, token);
        }

        public override Task<(bool keyExists, T cacheItem)> TryGetAsync<T>(
            string key,
            CancellationToken token = default)
        {
            return Task.Run(
                () =>
                {
                    var cacheItem = this.memoryCache.Get<CacheItem<T>>(key);
                    return (cacheItem != null, cacheItem?.Value);
                }, token);
        }

        public override Task SetAsync<T>(T cacheItem, string key, CancellationToken token = default)
        {
            return Task.Run(() => { this.memoryCache.Set(key, new CacheItem<T>(cacheItem)); }, token);
        }

        public override Task SetAsync<T>(T cacheItem, string key, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            return Task.Run(() => { this.memoryCache.Set(key, new CacheItem<T>(cacheItem), options.ToMemoryCache()); }, token);
        }

        public override Task<bool> ExistsAsync<T>(string key, CancellationToken token = default)
        {
            return Task.Run(
                () =>
                {
                    var cacheItem = this.memoryCache.Get<CacheItem<T>>(key);
                    return cacheItem != null;
                }, token);
        }

        public override Task RemoveAsync<T>(string key, CancellationToken token = default)
        {
            return Task.Run(() => { this.memoryCache.Remove(key); }, token);
        }

        #region IMemoryCache implementation

        public void Dispose()
        {
            this.memoryCache.Dispose();
        }

        public bool TryGetValue(object key, out object value)
        {
            return this.memoryCache.TryGetValue(key, out value);
        }

        public ICacheEntry CreateEntry(object key)
        {
            return this.memoryCache.CreateEntry(key);
        }

        public void Remove(object key)
        {
            this.memoryCache.Remove(key);
        }

        #endregion  IMemoryCache implementation
    }
}
