using System;
using System.Collections.Generic;

namespace Gwtdo.Scenarios
{
    public class Let
    {
        private readonly Dictionary<string, Lazy<object>> _objects;

        public object this[string key]
        {
            get => this;
            set => Add(key, value);
        }

        public Let()
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

        private Let Add(string key, object value)
        {
            _objects[NormalizeKey(key)] = new Lazy<object>(value);
            return this;
        }        

        private static string NormalizeKey(string key)
        {
            return key.StartsWith(":") ? key : $":{key}";
        }
    }
}