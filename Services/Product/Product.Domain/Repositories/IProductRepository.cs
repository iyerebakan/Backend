﻿using Data.EF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Repositories
{
    public interface IProductRepository : IRepository<Entities.Product>
    {
    }
}
