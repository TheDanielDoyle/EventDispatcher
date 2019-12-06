using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventDispatcher
{
    public class DefaultEventDispatcher : IEventDispatcher
    {
        public virtual void Dispatch<TEvent>(IEnumerable<TEvent> events, IEnumerable<IEventDispatchHandler<TEvent>> handlers) where TEvent : IEvent
        {
            IList<IEventDispatchHandler<TEvent>> handlerList = handlers.ToList();
            foreach (TEvent @event in events)
            {
                Dispatch(@event, handlerList);
            }
        }

        public virtual void Dispatch<TEvent>(TEvent @event, IEnumerable<IEventDispatchHandler<TEvent>> handlers) where TEvent : IEvent
        {
            foreach (IEventDispatchHandler<TEvent> handler in handlers.ToList())
            {
                handler.Handle(@event);
            }
        }

        public virtual async Task DispatchAsync<TEvent>(IEnumerable<TEvent> events, IEnumerable<IEventDispatchHandler<TEvent>> handlers, CancellationToken cancellation = default(CancellationToken)) where TEvent : IEvent
        {
            IList<IEventDispatchHandler<TEvent>> handlerList = handlers.ToList();
            foreach (TEvent @event in events)
            {
                await DispatchAsync(@event, handlerList, cancellation).ConfigureAwait(false);
            }
        }

        public virtual async Task DispatchAsync<TEvent>(TEvent @event, IEnumerable<IEventDispatchHandler<TEvent>> handlers, CancellationToken cancellation = default(CancellationToken)) where TEvent : IEvent
        {
            foreach (IEventDispatchHandler<TEvent> handler in handlers.ToList())
            {
                await handler.HandleAsync(@event, cancellation).ConfigureAwait(false);
            }
        }
    }
}
