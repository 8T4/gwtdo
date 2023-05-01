using System;

namespace Gwtdo.Scenarios.Attributes;

/// <summary>
/// Represents a custom attribute that can be applied to methods to specify conditions
/// that must be satisfied before the method can be run.
/// </summary>
/// <remarks>
/// The <see cref="ThenAttribute"/> can be used in the context of a testing framework or specification
/// framework to annotate test methods with a description of the preconditions that must be met
/// for the test to run.
/// </remarks>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ThenAttribute: Attribute, IGwtCustomAttribute
{
    /// <summary>
    /// Gets the description of the given condition.
    /// </summary>    
    public string Description { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ThenAttribute"/> class with the specified description
    /// of the given condition.
    /// </summary>
    /// <param name="description">The description of the given condition.</param>        
    public ThenAttribute(string description)
    {
        Description = description;
    }
}