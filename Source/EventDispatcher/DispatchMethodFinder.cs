using System;
using System.Linq;
using System.Reflection;

namespace EventDispatcher
{
    internal class DispatchMethodFinder : IDispatchMethodFinder
    {
        private static readonly MethodInfoCache methodInfoCache;

        static DispatchMethodFinder()
        {
            methodInfoCache = new MethodInfoCache();
        }

        public MethodInfo Find(Type eventType)
        {
            return methodInfoCache.AddOrGetExisting(eventType, DispatchMethodFactory);
        }

        public MethodInfo FindAsync(Type eventType)
        {
            return methodInfoCache.AddOrGetExisting(eventType, DispatchAsyncMethodFactory);
        }

        private static MethodInfo DispatchAsyncMethodFactory(Type eventType)
        {
            MethodInfo dispatchMethod = typeof(IEventDispatcher)
                .GetMethods()
                .FirstOrDefault(m => m.Name == nameof(IEventDispatcher.DispatchAsync) && m.GetParameters().First().Name == "event");
            return dispatchMethod?.MakeGenericMethod(new[] { eventType });
        }

        private static MethodInfo DispatchMethodFactory(Type eventType)
        {
            MethodInfo dispatchMethod = typeof(IEventDispatcher)
                .GetMethods()
                .FirstOrDefault(m => m.Name == nameof(IEventDispatcher.Dispatch) && m.GetParameters().First().Name == "event");
            return dispatchMethod?.MakeGenericMethod(new[] { eventType });
        }
    }
}