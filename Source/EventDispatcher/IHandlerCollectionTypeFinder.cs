using System;

namespace EventDispatcher
{
    internal interface IHandlerCollectionTypeFinder
    {
        Type Find(Type eventType);
    }
}