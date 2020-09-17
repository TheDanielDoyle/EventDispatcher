using System;

namespace EventDispatcher
{
    internal interface IValueFactoryCache<TKey, TValue>
    {
        TValue AddOrGetExisting(TKey key, Func<TKey, TValue> valueFactory);
    }
}