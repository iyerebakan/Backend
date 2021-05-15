using Data.EF.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Context
{
    public class ProductDbContext : DbContextBase
    {
        public ProductDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected ProductDbContext()
        {
        }

        public DbSet<Domain.Entities.Product> Products { get; set; }
    }
}
