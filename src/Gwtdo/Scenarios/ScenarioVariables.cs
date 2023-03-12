using System;
using System.Collections.Generic;

namespace Gwtdo.Scenarios;

public class ScenarioVariables
{
    private readonly Dictionary<string, Lazy<object>> _objects;

    public object this[string key]
    {
        set => Add(key, value);
        get => Contains(key) ? _objects[NormalizeKey(key)].Value : default;
    }

    public ScenarioVariables()
    {
        _objects = new Dictionary<string, Lazy<object>>();
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