using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.Utils
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            if (key == null || !dictionary.ContainsKey(key))
            {
                return defaultValue;
            }
            return dictionary[key];
        }
    }
}
