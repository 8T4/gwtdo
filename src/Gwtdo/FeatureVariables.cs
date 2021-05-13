using System;
using System.Collections.Generic;

namespace Gwtdo
{
    public class FeatureVariables
    {
        private readonly Dictionary<string, Lazy<object>> _objects;

        public object this[string key]
        {
            set => Add(key, value);
        }

        public FeatureVariables()
        {
            _objects = new Dictionary<string, Lazy<object>>();
        }

        public T Get<T>(string key)
        {
            return Contains(key) ? (T) _objects[NormalizeKey(key)].Value : default;
        }

        public string Replace(string input)
        {
            foreach (var (key, value) in _objects)
            {
                input = input.Replace(key, value.Value.ToString());
            }
            return input;
        }

        private bool Contains(string key)
        {
            return _objects.ContainsKey(NormalizeKey(key));
        }

        private void Add(string key, object value)
        {
            _objects[NormalizeKey(key)] = new Lazy<object>(value);
        }        

        private static string NormalizeKey(string key)
        {
            return key.StartsWith(":") ? key : $":{key}";
        }
    }
}