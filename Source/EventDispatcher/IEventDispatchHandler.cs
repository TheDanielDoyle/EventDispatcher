using System.Threading;
using System.Threading.Tasks;

namespace EventDispatcher
{
    public interface IEventDispatchHandler<in TEvent> : IEventDispatchHandler
    {
        void Handle(TEvent @event);

        Task HandleAsync(TEvent @event, CancellationToken cancellation = default(CancellationToken));
    }

    public interface IEventDispatchHandler
    {
    }
}
