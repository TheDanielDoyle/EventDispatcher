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

        protected DefaultEventDispatcherContext(IEventDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public void Dispatch<TEvent>() where TEvent : IEvent
        {
            try
            {
                foreach (IEvent @event in GetEvents(typeof(TEvent)))
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
                foreach (IEvent @event in GetEvents(typeof(TEvent)))
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

        private Type CreateHandlerCollectionType(Type eventType)
        {
            Type genericEnumerableType = typeof(IEnumerable<>);
            Type genericHandlerType = typeof(IEventDispatchHandler<>);
            Type concreteHandlerType = genericHandlerType.MakeGenericType(eventType);
            return genericEnumerableType.MakeGenericType(concreteHandlerType);
        }

        private MethodInfo GetDispatchAsyncMethod(Type eventType)
        {
            MethodInfo dispatchMethod = typeof(IEventDispatcher)
                .GetMethods()
                .FirstOrDefault(m => m.Name == nameof(IEventDispatcher.DispatchAsync) && m.GetParameters().First().Name == "event");
            return dispatchMethod?.MakeGenericMethod(new[] { eventType });
        }

        private MethodInfo GetDispatchMethod(Type eventType)
        {
            MethodInfo dispatchMethod = typeof(IEventDispatcher)
                .GetMethods()
                .FirstOrDefault(m => m.Name == nameof(IEventDispatcher.Dispatch) && m.GetParameters().First().Name == "event");
            return dispatchMethod?.MakeGenericMethod(new[] { eventType });
        }

        private IEventDispatchHandler[] GetHandlers(Type eventType)
        {
            Type handlerCollectionType = CreateHandlerCollectionType(eventType);
            return CreateHandlers(handlerCollectionType);
        }

        private void InvokeDispatch(IEvent @event)
        {
            Type eventType = @event.GetType();
            IEventDispatchHandler[] handlers = GetHandlers(eventType);
            MethodInfo dispatchMethod = GetDispatchMethod(eventType);
            if (dispatchMethod == null)
            {
                throw new EventDispatcherContextException($"Unable to find {nameof(IEventDispatcher.Dispatch)} method using reflection.");
            }
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
            MethodInfo dispatchMethod = GetDispatchAsyncMethod(eventType);
            if (dispatchMethod == null)
            {
                throw new EventDispatcherContextException($"Unable to find {nameof(IEventDispatcher.DispatchAsync)} method using reflection.");
            }
            if (handlers == null || handlers.Length == 0)
            {
                throw new EventDispatcherContextException($"There are no handlers registered to handle {@event.GetType().Name}");
            }
            return (Task)dispatchMethod.Invoke(this.dispatcher, new[] { @event as object, handlers, cancellationToken });
        }
    }
}
