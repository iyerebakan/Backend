using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cache
{
    internal class DistributedCacheService : CacheServiceBase, IDistributedCacheService, IDistributedCache
    {
        private readonly IDistributedCache distributedCache;

        public DistributedCacheService(IOptions<RedisCacheOptions> options)
        {
            this.distributedCache = new RedisCache(options);
        }

        public override async Task<T> GetAsync<T>(
           string key,
           CancellationToken token = default)
        {
            var data = await this.distributedCache.GetStringAsync(key, token).ConfigureAwait(false);
            if (string.IsNullOrEmpty(data))
            {
                throw new Exception(key);
            }

            return JObject.Parse(data).ToObject<CacheItem<T>>().Value;
        }

        public override async Task<(bool keyExists, T cacheItem)> TryGetAsync<T>(
            string key,
            CancellationToken token = default)
        {
            var data = await this.distributedCache.GetStringAsync(key, token).ConfigureAwait(false);
            return string.IsNullOrEmpty(data)
                ? (false, default(T))
                : (true, JObject.Parse(data).ToObject<CacheItem<T>>().Value);
        }

        public override Task SetAsync<T>(T cacheItem, string key, CancellationToken token = default)
        {
            var data = JObject.FromObject(new CacheItem<T>(cacheItem)).ToString();
            return this.distributedCache.SetStringAsync(key, data, token);
        }

        public override async Task<bool> ExistsAsync<T>(string key, CancellationToken token = default)
        {
            var data = await this.distributedCache.GetStringAsync(key, token).ConfigureAwait(false);
            return !string.IsNullOrEmpty(data);
        }

        public override Task RemoveAsync<T>(string key, CancellationToken token = default)
        {
            return this.distributedCache.RemoveAsync(key, token);
        }

        public override Task SetAsync<T>(T cacheItem, string key, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            var data = JObject.FromObject(new CacheItem<T>(cacheItem)).ToString();
            return this.distributedCache.SetStringAsync(key, data, options, token);
        }

        #region IDistributedCache implementation

        public byte[] Get(string key)
        {
            return this.distributedCache.Get(key);
        }

        public Task<byte[]> GetAsync(string key, CancellationToken token = default)
        {
            return this.distributedCache.GetAsync(key, token);
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            this.distributedCache.Set(key, value, options);
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            return this.distributedCache.SetAsync(key, value, options, token);
        }

        public void Refresh(string key)
        {
            this.distributedCache.Refresh(key);
        }

        public Task RefreshAsync(string key, CancellationToken token = default)
        {
            return this.distributedCache.RefreshAsync(key, token);
        }

        public void Remove(string key)
        {
            this.distributedCache.Remove(key);
        }

        public Task RemoveAsync(string key, CancellationToken token = default)
        {
            return this.distributedCache.RefreshAsync(key, token);
        }

        #endregion IDistributedCache implementation
    }
}
