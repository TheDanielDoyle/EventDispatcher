using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventDispatcher
{
    public interface IEventDispatcher
    {
        Task Dispatch<TEvent>(IEnumerable<TEvent> events, IEventDispatchInvoker<TEvent> invoker, CancellationToken cancellation)
            where TEvent : IEvent;

        Task Dispatch<TEvent>(TEvent @event, IEventDispatchInvoker<TEvent> invoker, CancellationToken cancellation)
            where TEvent : IEvent;
    }
}