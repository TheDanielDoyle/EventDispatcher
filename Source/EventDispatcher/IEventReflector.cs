using System;
using System.Reflection;

namespace EventDispatcher
{
    public interface IEventReflector
    {
        MethodInfo GetDispatchAsyncMethod(Type eventType);

        MethodInfo GetDispatchMethod(Type eventType);

        Type GetHandlerCollectionType(Type eventType);
    }
}