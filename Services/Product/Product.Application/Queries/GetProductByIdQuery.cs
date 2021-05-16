using CQRS.Abstraction;
using CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Queries
{
    public class GetProductByIdQuery : IQuery<Domain.Entities.Product>,ICacheable
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
