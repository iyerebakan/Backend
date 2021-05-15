using CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Queries
{
    public class GetAllProductQuery : IQuery<List<Domain.Entities.Product>>
    {
    }
}
