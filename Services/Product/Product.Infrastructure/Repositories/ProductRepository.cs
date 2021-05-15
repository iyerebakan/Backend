using Data.EF.Context;
using Data.EF.Repository;
using Product.Domain.Repositories;
using Product.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repositories
{
    public class ProductRepository : EntityRepository<Domain.Entities.Product>, IProductRepository
    {
        public ProductRepository(ProductDbContext dbContext) : base(dbContext)
        {
        }
    }
}
