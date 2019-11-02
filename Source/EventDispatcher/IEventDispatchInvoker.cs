using System.Threading;
using System.Threading.Tasks;

namespace EventDispatcher
{
    public interface IEventDispatchInvoker<in TEvent>
        where TEvent : IEvent
    {
        void Invoke(TEvent @event);

        Task InvokeAsync(TEvent @event, CancellationToken cancellation = default(CancellationToken));
    }
}
