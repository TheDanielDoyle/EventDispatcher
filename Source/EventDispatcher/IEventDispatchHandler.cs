using System.Threading;
using System.Threading.Tasks;

namespace EventDispatcher
{
    public interface IEventDispatchHandler<in TEvent> : IEventDispatcherHandler
        where TEvent : IEvent
    {
        void Handle(TEvent @event);

        Task HandleAsync(TEvent @event, CancellationToken cancellation = default(CancellationToken));
    }

    public interface IEventDispatcherHandler
    {
    }
}
