using System;
using System.Reflection;

namespace EventDispatcher
{
    public class EventReflector : IEventReflector
    {
        private IDispatchMethodFinder dispatchMethodFinder;
        private IHandlerCollectionTypeFinder handlerCollectionTypeFinder;

        public EventReflector()
        {
            this.dispatchMethodFinder = new DispatchMethodFinder();
            this.handlerCollectionTypeFinder = new HandlerCollectionTypeFinder();
        }

        public MethodInfo GetDispatchAsyncMethod(Type eventType)
        {
            return dispatchMethodFinder.FindAsync(eventType);
        }

        public MethodInfo GetDispatchMethod(Type eventType)
        {
            return dispatchMethodFinder.Find(eventType);
        }

        public Type GetHandlerCollectionType(Type eventType)
        {
            return this.handlerCollectionTypeFinder.Find(eventType);
        }
    }
}