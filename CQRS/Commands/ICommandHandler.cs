using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Commands
{
    public interface ICommandHandler<in T> : IRequestHandler<T>
        where T : ICommand
    {
    }
}
