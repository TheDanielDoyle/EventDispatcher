using System;
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

        public IEventCollection AssignableTo<TEvent>() where TEvent : IEvent
        {
            return AssignableTo(typeof(TEvent));
        }

        public IEventCollection AssignableTo(Type eventType)
        {
            if (eventType.IsInstanceOfType(typeof(IEvent)))
            {
                throw new ArgumentException($"{eventType.Name} is not an instance of {nameof(IEvent)}");
            }
            return new EventCollection(this.Where(eventType.IsInstanceOfType));
        }

        public IEventCollection OfType<TEvent>() where TEvent : IEvent
        {
            return OfType(typeof(TEvent));
        }

        public IEventCollection OfType(Type eventType)
        {
            if (eventType.IsInstanceOfType(typeof(IEvent)))
            {
                throw new ArgumentException($"{eventType.Name} is not an instance of {nameof(IEvent)}");
            }
            return new EventCollection(this.Where(t => GetType() == eventType));
        }
    }
}