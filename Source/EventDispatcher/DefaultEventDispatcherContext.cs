using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EventDispatcher
{
    public abstract class DefaultEventDispatcherContext : IEventDispatcherContext
    {
        private readonly IEventDispatcher dispatcher;
        private readonly IEventReflector eventReflector;

        protected DefaultEventDispatcherContext(IEventDispatcher dispatcher) : this(dispatcher, new EventReflector())
        {
        }

        protected DefaultEventDispatcherContext(IEventDispatcher dispatcher, IEventReflector eventReflector)
        {
            this.dispatcher = dispatcher;
            this.eventReflector = eventReflector;
        }

        public void Dispatch<TEvent>() where TEvent : IEvent
        {
            try
            {
                foreach (IEvent @event in GetEvents(typeof(TEvent)).ToList())
                {
                    InvokeDispatch(@event);
                }
            }
            catch (Exception exception)
            {
                throw new EventDispatcherContextException(exception.Message, exception);
            }
        }

        public async Task DispatchAsync<TEvent>(CancellationToken cancellationToken = default(CancellationToken)) where TEvent : IEvent
        {
            try
            {
                foreach (IEvent @event in GetEvents(typeof(TEvent)).ToList())
                {
                    await InvokeDispatchAsync(@event, cancellationToken).ConfigureAwait(false);
                }
            }
            catch (Exception exception)
            {
                throw new EventDispatcherContextException(exception.Message, exception);
            }
        }

        protected abstract IEventDispatchHandler[] CreateHandlers(Type handlerCollectionType);

        protected abstract IEnumerable<IEvent> GetEvents(Type eventType);

        private IEventDispatchHandler[] GetHandlers(Type eventType)
        {
            Type handlerCollectionType = this.eventReflector.GetHandlerCollectionType(eventType);
            return CreateHandlers(handlerCollectionType);
        }

        private void InvokeDispatch(IEvent @event)
        {
            Type eventType = @event.GetType();
            IEventDispatchHandler[] handlers = GetHandlers(eventType);
            MethodInfo dispatchMethod = this.eventReflector.GetDispatchMethod(eventType);
            if (handlers == null || handlers.Length == 0)
            {
                throw new EventDispatcherContextException($"There are no handlers registered to handle {@event.GetType().Name}");
            }
            dispatchMethod.Invoke(this.dispatcher, new[] { @event as object });
        }

        private Task InvokeDispatchAsync(IEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            Type eventType = @event.GetType();
            IEventDispatchHandler[] handlers = GetHandlers(eventType);
            MethodInfo dispatchMethod = this.eventReflector.GetDispatchAsyncMethod(eventType);
            if (handlers == null || handlers.Length == 0)
            {
                throw new EventDispatcherContextException($"There are no handlers registered to handle {@event.GetType().Name}");
            }
            return (Task)dispatchMethod.Invoke(this.dispatcher, new[] { @event as object, handlers, cancellationToken });
        }
    }
}