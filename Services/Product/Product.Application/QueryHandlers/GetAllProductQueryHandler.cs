using Cache;
using CQRS.Queries;
using Product.Application.Queries;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.QueryHandlers
{
    public class GetAllProductQueryHandler : IQueryHandler<GetAllProductQuery, Domain.Entities.Product>
    {
        private readonly IProductRepository productRepository;
        private readonly ICacheService cacheService;

        public GetAllProductQueryHandler(IProductRepository productRepository, ICacheService cacheService)
        {
            this.productRepository = productRepository;
            this.cacheService = cacheService;
        }

        public async Task<Domain.Entities.Product> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var (keyExists, cacheItem) = await this.cacheService.TryGetAsync<Domain.Entities.Product>(nameof(GetAllProductQuery), cancellationToken);
            if (keyExists) return cacheItem;
            else
            {
                var list = await productRepository.GetListAsync();
                await this.cacheService.SetAsync(list.First(), nameof(GetAllProductQuery), cancellationToken).ConfigureAwait(false);
                return list.First();
            }
        }
    }
}
