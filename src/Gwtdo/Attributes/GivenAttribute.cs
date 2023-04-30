using System;

namespace Gwtdo.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class GivenAttribute: Attribute, IGwtCustomAttribute
{
    public string Description { get; }

    public GivenAttribute(string description)
    {
        Description = description;
    }
}