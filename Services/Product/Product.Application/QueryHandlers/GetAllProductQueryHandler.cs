using Cache;
using CQRS.Abstraction;
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

        public GetAllProductQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<List<Domain.Entities.Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var response = await productRepository.GetListAsync();
            return response.ToList();
        }
    }
}
