using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Template.Web.Models
{
    public static class Extensions
    {
        public static TValue GetValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
            where TValue : class
        {
            TValue value;
            if (dictionary == null || key == null) return null;
            if (dictionary.TryGetValue(key, out value)) return value;
            return null;
        }
    }
}