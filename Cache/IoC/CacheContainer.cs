using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.IoC
{
    public static class CacheContainer
    {
        public static void AddBackendCache(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<MemoryCacheOptions>(options => configuration.GetSection("MemoryCacheOptions").Bind(options));
            services.Configure<RedisCacheOptions>(options => configuration.GetSection("RedisCacheOptions").Bind(options));
            services.AddSingleton<IDistributedCacheService, DistributedCacheService>();
            services.AddSingleton<IDistributedCache, DistributedCacheService>();
            services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
            services.AddSingleton<IMemoryCache, MemoryCacheService>();
            services.AddSingleton<ICacheService, CacheService>();
        }
    }
}
