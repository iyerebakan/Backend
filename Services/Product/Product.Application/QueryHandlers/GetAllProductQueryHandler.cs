using CQRS.Queries;
using Product.Application.Queries;
using Product.Domain.Dtos;
using Product.Domain.Repositories;
using Product.Infrastructure.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.QueryHandlers
{
    public class GetAllProductQueryHandler : IQueryHandler<GetAllProductQuery, List<ProductDto>>
    {
        private readonly IProductRepository productRepository;

        public GetAllProductQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var response = await productRepository.GetListAsync(selector: ProductExpressions.ProductDtos);
            return response.ToList();
        }
    }
}
