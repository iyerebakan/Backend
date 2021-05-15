using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cache
{
    internal class CacheService : CacheServiceBase, ICacheService
    {
        private readonly IMemoryCacheService memoryCache;

        private readonly IDistributedCacheService distributedCache;

        public CacheService(IMemoryCacheService memoryCache, IDistributedCacheService distributedCache)
        {
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        public override async Task<T> GetAsync<T>(string key, CancellationToken token = default)
        {
            try
            {
                var (keyExist, cacheItem) = await this.memoryCache.TryGetAsync<T>(key, token).ConfigureAwait(false);
                if (keyExist)
                {
                    return cacheItem;
                }

                cacheItem = await this.distributedCache.GetAsync<T>(key, token).ConfigureAwait(false);
                await this.memoryCache.SetAsync(cacheItem, key, token).ConfigureAwait(false);

                return cacheItem;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public override async Task<(bool keyExists, T cacheItem)> TryGetAsync<T>(string key, CancellationToken token = default)
        {
            try
            {
                var result = await this.memoryCache.TryGetAsync<T>(key, token).ConfigureAwait(false);
                if (result.keyExists)
                {
                    return result;
                }

                result = await this.distributedCache.TryGetAsync<T>(key, token).ConfigureAwait(false);
                if (result.keyExists)
                {
                    await this.memoryCache.SetAsync(result.cacheItem, key, token).ConfigureAwait(false);
                }

                return result;
            }
            catch (Exception)
            {
                return (false, null);
            }
            
        }

        public override async Task SetAsync<T>(T cacheItem, string key, CancellationToken token = default)
        {
            try
            {
                await this.memoryCache.SetAsync(cacheItem, key, token).ConfigureAwait(false);
                await this.distributedCache.SetAsync(cacheItem, key, token).ConfigureAwait(false);
            }
            catch (Exception)
            {
                
            }
           
        }

        public override async Task SetAsync<T>(T cacheItem, string key, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            try
            {
                await this.memoryCache.SetAsync(cacheItem, key, options, token).ConfigureAwait(false);
                await this.distributedCache.SetAsync(cacheItem, key, options, token).ConfigureAwait(false);
            }
            catch (Exception)
            {

            }
          
        }

        public override async Task<bool> ExistsAsync<T>(string key, CancellationToken token = default)
        {
            try
            {
                return await this.memoryCache.ExistsAsync<T>(key, token).ConfigureAwait(false) || await this.distributedCache.ExistsAsync<T>(key, token).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override async Task RemoveAsync<T>(string key, CancellationToken token = default)
        {
            try
            {
                await this.memoryCache.RemoveAsync<T>(key, token).ConfigureAwait(false);
                await this.distributedCache.RemoveAsync<T>(key, token).ConfigureAwait(false);
            }
            catch (Exception)
            {
            }
            
        }
    }
}
