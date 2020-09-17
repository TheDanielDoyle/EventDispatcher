using System;
using System.Collections.Generic;

namespace EventDispatcher
{
    internal class HandlerCollectionTypeFinder : IHandlerCollectionTypeFinder
    {
        private static readonly TypeCache typeCache;

        static HandlerCollectionTypeFinder()
        {
            typeCache = new TypeCache();
        }

        public Type Find(Type eventType)
        {
            return typeCache.AddOrGetExisting(eventType, HandlerCollectionTypeFactory);
        }

        private static Type HandlerCollectionTypeFactory(Type eventType)
        {
            Type genericEnumerableType = typeof(IEnumerable<>);
            Type genericHandlerType = typeof(IEventDispatchHandler<>);
            Type concreteHandlerType = genericHandlerType.MakeGenericType(eventType);
            return genericEnumerableType.MakeGenericType(concreteHandlerType);
        }
    }
}