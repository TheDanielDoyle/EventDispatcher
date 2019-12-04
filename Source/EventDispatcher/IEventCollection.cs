using System.Collections.Generic;

namespace EventDispatcher
{
    public interface IEventCollection : IReadOnlyCollection<IEvent>
    {
        IEventCollection OfType<TEvent>();
    }
}