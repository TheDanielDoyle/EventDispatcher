using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventDispatcher
{
    public class EventDispatcher : IEventDispatcher
    {
        public async Task Dispatch<TEvent>(IEnumerable<TEvent> events, IEventDispatchInvoker<TEvent> invoker,  CancellationToken cancellation) 
            where TEvent : IEvent
        {
            foreach (TEvent @event in events)
            {
                await Dispatch(@event, invoker, cancellation);
            }
        }

        public async Task Dispatch<TEvent>(TEvent @event, IEventDispatchInvoker<TEvent> invoker, CancellationToken cancellation) 
            where TEvent : IEvent
        {
            await invoker.Invoke(@event, cancellation);
        }
    }
}
