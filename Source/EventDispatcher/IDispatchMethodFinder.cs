using System;
using System.Reflection;

namespace EventDispatcher
{
    internal interface IDispatchMethodFinder
    {
        MethodInfo Find(Type eventType);

        MethodInfo FindAsync(Type eventType);
    }
}