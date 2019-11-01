using System;

namespace EventDispatcher
{
    public interface IEvent
    {
        DateTimeOffset Created { get; }
    }
}
