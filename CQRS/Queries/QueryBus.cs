using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Queries
{
    public class QueryBus : IQueryBus
    {
        private readonly IMediator _mediator;

        public QueryBus(IMediator mediator)
        {
            _mediator = mediator;
        }


        public Task<TResponse> Send<TResponse>(IQuery<TResponse> query)
        {
            return _mediator.Send(query);
        }
    }
}
