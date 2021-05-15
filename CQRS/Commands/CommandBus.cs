using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly IMediator _mediator;

        public CommandBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            return _mediator.Send(command);
        }
    }
}
