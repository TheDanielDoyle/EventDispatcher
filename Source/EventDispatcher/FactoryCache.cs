using System;
using System.Collections.Generic;

namespace EventDispatcher
{
    internal class FactoryCache<TKey, TValue> : IValueFactoryCache<TKey, TValue>
    {
        private static readonly IDictionary<TKey, TValue> cache;

        static FactoryCache()
        {
            cache = new Dictionary<TKey, TValue>();
        }

        public TValue AddOrGetExisting(TKey key, Func<TKey, TValue> valueFactory)
        {
            if (!cache.TryGetValue(key, out TValue value))
            {
                value = valueFactory.Invoke(key);
                if (value == null)
                {
                    throw new FactoryCacheException($"Unable to construct value using key: {nameof(TKey)}.");
                }
                cache.Add(key, value);
            }
            return value;
        }
    }
}