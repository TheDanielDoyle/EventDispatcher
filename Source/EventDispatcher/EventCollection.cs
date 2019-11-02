using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EventDispatcher
{
    public class EventCollection : ReadOnlyCollection<IEvent>, IEventCollection
    {
        public EventCollection(IEnumerable<IEvent> events) : base(events.ToList())
        {
        }

        public EventCollection(IList<IEvent> events) : base(events)
        {
        }

        public IEventCollection OfType<TEvent>()
            where TEvent : IEvent
        {
            return new EventCollection(this.Where(t => GetType() == typeof(TEvent)));
        }
    }
}