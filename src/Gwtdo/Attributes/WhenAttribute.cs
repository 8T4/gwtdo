using System;

namespace Gwtdo.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class WhenAttribute: Attribute, IGwtCustomAttribute
{
    public string Description { get; }

    public WhenAttribute(string description)
    {
        Description = description;
    }
}