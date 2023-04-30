using System;

namespace Gwtdo.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ThenAttribute: Attribute, IGwtCustomAttribute
{
    public string Description { get; }

    public ThenAttribute(string description)
    {
        Description = description;
    }
}