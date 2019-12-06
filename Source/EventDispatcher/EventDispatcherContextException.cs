using System;

namespace EventDispatcher
{
    public sealed class EventDispatcherContextException : Exception
    {
        public EventDispatcherContextException(string message) : base(message)
        {
        }

        public EventDispatcherContextException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}