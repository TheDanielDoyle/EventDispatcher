using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventDispatcher
{
    public interface IEventDispatcher
    {
        void Dispatch<TEvent>(IEnumerable<TEvent> events, IEventDispatchInvoker<TEvent> invoker)
            where TEvent : IEvent;

        void Dispatch<TEvent>(TEvent @event, IEventDispatchInvoker<TEvent> invoker)
            where TEvent : IEvent;

        Task DispatchAsync<TEvent>(IEnumerable<TEvent> events, IEventDispatchInvoker<TEvent> invoker, CancellationToken cancellation = default(CancellationToken))
            where TEvent : IEvent;

        Task DispatchAsync<TEvent>(TEvent @event, IEventDispatchInvoker<TEvent> invoker, CancellationToken cancellation = default(CancellationToken))
            where TEvent : IEvent;
    }
}