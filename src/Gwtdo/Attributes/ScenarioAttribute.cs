using System;

namespace Gwtdo.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class ScenarioAttribute: Attribute, IGwtCustomAttribute
{
    public string Description { get; }

    public ScenarioAttribute(string description)
    {
        Description = description;
    }
}