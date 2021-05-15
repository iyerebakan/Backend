using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Events
{
    public interface IEventBus
    {
        Task Publish<TEvent>(params TEvent[] events) where TEvent : IEvent;
    }
}
