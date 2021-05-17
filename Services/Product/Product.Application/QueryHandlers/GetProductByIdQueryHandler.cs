using CQRS.Queries;
using Product.Application.Queries;
using Product.Domain.Dtos;
using Product.Domain.Repositories;
using Product.Infrastructure.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Application.QueryHandlers
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await this.productRepository.GetAsync(t => t.Id == request.Id, selector: ProductExpressions.ProductDtos);
        }
    }
}
