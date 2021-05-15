using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF.Context
{
    public class DbContextBase : DbContext
    {
        public DbContextBase([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected DbContextBase()
        {
        }
    }
}
