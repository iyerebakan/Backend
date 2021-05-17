using Cache.Abstraction;
using CQRS.Queries;
using Product.Domain.Dtos;

namespace Product.Application.Queries
{
    public class GetProductByIdQuery : IQuery<ProductDto>, ICacheable
    {
        public GetProductByIdQuery(int id)
        {
            this.Id = id;
        }
        public GetProductByIdQuery()
        {

        }
        public int Id { get; private set; }

        public string CacheKey => $"{nameof(GetProductByIdQuery)}-{this.Id}";
    }
}
