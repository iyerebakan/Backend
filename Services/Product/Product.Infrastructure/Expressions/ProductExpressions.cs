using Product.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Expressions
{
    public static class ProductExpressions
    {
        public static Expression<Func<Domain.Entities.Product, ProductDto>> ProductDtos
        {
            get
            {
                return c => new ProductDto()
                {
                    Id = c.Id,
                    Active = c.Active,
                    Definition = c.Definition,
                    Price = c.Price,
                    ProductGroupId = c.ProductGroupId  
                };
            }
        }
    }
}
