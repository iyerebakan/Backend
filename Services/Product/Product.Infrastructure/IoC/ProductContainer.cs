using Microsoft.Extensions.DependencyInjection;
using Product.Domain.Repositories;
using Product.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.IoC
{
    public static class ProductContainer
    {
        public static void AddBackendProduct(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
