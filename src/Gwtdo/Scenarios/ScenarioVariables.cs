using System;
using System.Collections.Generic;

namespace Gwtdo.Scenarios;

public sealed class ScenarioVariables
{
    private readonly Dictionary<string, Lazy<object?>> _objects;

    public object? this[string key]
    {
        set => Add(key, value);
        get => Contains(key) ? _objects[NormalizeKey(key)].Value : default;
    }

    public ScenarioVariables()
    {
        _objects = new Dictionary<string, Lazy<object?>>();
    }

    public string Replace(string input)
    {
        foreach (var (key, lazy) in _objects)
        {
            input = input.Replace(key, lazy.Value?.ToString());
        }
        return input;
    }

    public void Add(object value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        var properties = value.GetType().GetProperties();
        foreach (var propertyInfo in properties)
            Add(propertyInfo.Name, propertyInfo.GetValue(value));        
    }

    private bool Contains(string key)
    {
        return _objects.ContainsKey(NormalizeKey(key));
    }

    private void Add(string key, object? value)
    {
        _objects[NormalizeKey(key)] = new Lazy<object?>(() => value);
    }        

    private static string NormalizeKey(string key)
    {
        return key.StartsWith(":") ? key : $":{key}";
    }
}