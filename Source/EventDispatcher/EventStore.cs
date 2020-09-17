using System.Collections.Generic;

namespace EventDispatcher
{
    public class EventStore : IEventStore
    {
        private readonly IList<IEvent> events;

        public EventStore()
        {
            this.events = new List<IEvent>();
            Events = new EventCollection(events);
        }

        public IEventCollection Events { get; }

        public void AddEvent(IEvent @event)
        {
            events.Add(@event);
        }

        public void AddEvents(IEnumerable<IEvent> events)
        {
            foreach (IEvent @event in events)
            {
                AddEvent(@event);
            }
        }

        public void ClearEvents()
        {
            this.events.Clear();
        }
    }
}