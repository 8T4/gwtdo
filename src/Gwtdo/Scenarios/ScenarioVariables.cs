using System;
using System.Collections.Generic;

namespace Gwtdo.Scenarios;

/// <summary>
/// Represents a collection of lazy-loaded objects that can be used to define and access variables within a scenario.
/// </summary>
public sealed class ScenarioVariables
{
    private readonly IDictionary<string, Lazy<object?>> _objects;

    /// <summary>
    /// Gets or sets the object associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the object to get or set.</param>
    /// <returns>The object associated with the specified key, or <see langword="null"/> if the key is not found.</returns>
    public object? this[string key]
    {
        set => Load(key, value);
        get => Contains(key) ? _objects[NormalizeKey(key)].Value : default;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ScenarioVariables"/> class.
    /// </summary>
    public ScenarioVariables()
    {
        _objects = new Dictionary<string, Lazy<object?>>();
    }

    /// <summary>
    /// Replaces all occurrences of keys in the input string with the associated objects.
    /// </summary>
    /// <param name="input">The string in which to replace the keys.</param>
    /// <returns>A copy of the input string with all keys replaced with their associated objects.</returns>
    internal string Replace(string input)
    {
        foreach (var (key, lazy) in _objects)
        {
            input = input.Replace(key, lazy.Value?.ToString());
        }
        return input;
    }

    /// <summary>
    /// Adds the properties of the specified object to this <see cref="ScenarioVariables"/> instance.
    /// </summary>
    /// <param name="value">The object whose properties to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is <see langword="null"/>.</exception>
    public void Load(object value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        var properties = value.GetType().GetProperties();
        foreach (var propertyInfo in properties)
            Load(propertyInfo.Name, propertyInfo.GetValue(value));        
    }

    private bool Contains(string key) => _objects.ContainsKey(NormalizeKey(key));
    private void Load(string key, object? value) => _objects[NormalizeKey(key)] = new Lazy<object?>(() => value);
    private static string NormalizeKey(string key) => key.StartsWith(":") ? key : $":{key}";
}
