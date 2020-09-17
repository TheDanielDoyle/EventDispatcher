using System;

namespace EventDispatcher
{
    internal class FactoryCacheException : Exception
    {
        public FactoryCacheException(string message) : base(message)
        {
        }
    }
}