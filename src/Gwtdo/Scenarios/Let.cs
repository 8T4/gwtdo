using System.Collections.Generic;

namespace Gwtdo.Scenarios
{
    public class Let
    {
        private readonly Dictionary<string, object> _objects;

        public object this[string key]
        {
            get => this;
            set => Add(key, value);
        }

        public Let()
        {
            _objects = new Dictionary<string, object>();
        }

        public T Get<T>(string key)
        {
            return Contains(key) ? (T) _objects[NormalizeKey(key)] : default;
        }

        public string Replace(string input)
        {
            foreach (var (key, value) in _objects)
            {
                input = input.Replace(key, value.ToString());
            }
            return input;
        }

        private bool Contains(string key)
        {
            return _objects.ContainsKey(NormalizeKey(key));
        }

        private Let Add(string key, object value)
        {
            _objects[NormalizeKey(key)] = value;
            return this;
        }        

        private static string NormalizeKey(string key)
        {
            return key.StartsWith(":") ? key : $":{key}";
        }

        public static implicit operator Let((string, object)[] tupla)
        {
            var let = new Let();
            foreach (var (key, value) in tupla)
            {
                let.Add(key, value);
            }

            return let;
        }
    }
}