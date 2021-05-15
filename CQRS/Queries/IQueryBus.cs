using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Queries
{
    public interface IQueryBus
    {
        Task<TResponse> Send<TResponse>(IQuery<TResponse> query);
    }
}
