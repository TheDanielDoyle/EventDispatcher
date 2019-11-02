using System;
using System.Collections.Generic;

namespace EventDispatcher
{
    public interface IEventStore
    {
        IEventCollection Events { get; }

        void AddEvent(IEvent @event);

        void AddEvents(IEnumerable<IEvent> events);
    }
}
