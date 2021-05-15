using Cache;
using CQRS.Queries;
using Microsoft.Extensions.Logging;
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
    public class GetAllProductQueryHandler : IQueryHandler<GetAllProductQuery, List<Domain.Entities.Product>>
    {
        private readonly IProductRepository productRepository;
        private readonly ICacheService cacheService;
        private readonly ILogger<GetAllProductQuery> logger;

        public GetAllProductQueryHandler(IProductRepository productRepository, ICacheService cacheService, ILogger<GetAllProductQuery> logger)
        {
            this.productRepository = productRepository;
            this.cacheService = cacheService;
            this.logger = logger;
        }

        public async Task<List<Domain.Entities.Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Handle Product Started");
            var product = new List<Domain.Entities.Product>();
            var (keyExists, cacheItem) = await this.cacheService.TryGetAsync<List<Domain.Entities.Product>>(nameof(GetAllProductQuery), cancellationToken);
            if (keyExists)
            {
                product = cacheItem;
            }
            else
            {
                var list = await productRepository.GetListAsync();
                await this.cacheService.SetAsync(list.ToList(), nameof(GetAllProductQuery), cancellationToken).ConfigureAwait(false);
                product = list.ToList();
            }
            this.logger.LogInformation("Handle Product Ended");
            return product;
        }
    }
}
