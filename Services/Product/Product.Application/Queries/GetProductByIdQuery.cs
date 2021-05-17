using CQRS.Abstraction;
using CQRS.Queries;
using Product.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Queries
{
    public class GetProductByIdQuery : IQuery<ProductDto>,ICacheable
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
