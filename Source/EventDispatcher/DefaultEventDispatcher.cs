using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventDispatcher
{
    public class DefaultEventDispatcher : IEventDispatcher
    {
        public void Dispatch<TEvent>(IEnumerable<TEvent> events, IEnumerable<IEventDispatchHandler<TEvent>> handlers)
        {
            IList<IEventDispatchHandler<TEvent>> handlerList = handlers.ToList();
            foreach (TEvent @event in events)
            {
                Dispatch(@event, handlerList);
            }
        }

        public void Dispatch<TEvent>(TEvent @event, IEnumerable<IEventDispatchHandler<TEvent>> handlers)
        {
            foreach (IEventDispatchHandler<TEvent> handler in handlers.ToList())
            {
                handler.Handle(@event);
            }
        }

        public async Task DispatchAsync<TEvent>(IEnumerable<TEvent> events, IEnumerable<IEventDispatchHandler<TEvent>> handlers, CancellationToken cancellation = default(CancellationToken))
        {
            IList<IEventDispatchHandler<TEvent>> handlerList = handlers.ToList();
            foreach (TEvent @event in events)
            {
                await DispatchAsync(@event, handlerList, cancellation).ConfigureAwait(false);
            }
        }

        public async Task DispatchAsync<TEvent>(TEvent @event, IEnumerable<IEventDispatchHandler<TEvent>> handlers, CancellationToken cancellation = default(CancellationToken))
        {
            foreach (IEventDispatchHandler<TEvent> handler in handlers.ToList())
            {
                await handler.HandleAsync(@event, cancellation).ConfigureAwait(false);
            }
        }
    }
}
