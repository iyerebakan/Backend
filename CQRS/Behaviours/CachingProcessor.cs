using Cache;
using CQRS.Abstraction;
using CQRS.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Behaviours
{
    public class CachingProcessor<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : class, new()
        where TRequest : ICacheable
    {
        private readonly ICacheService cacheService;

        public CachingProcessor(ICacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var (keyExists, cacheItem) = await this.cacheService.TryGetAsync<TResponse>(request.CacheKey, cancellationToken);
            if (keyExists) return cacheItem;

            else
            {
                var response = await next();
                await this.cacheService.SetAsync(response, request.CacheKey, cancellationToken)
                                .ConfigureAwait(false);
                return response;
            }
        }
    }
}
