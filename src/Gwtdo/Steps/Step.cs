namespace Gwtdo.Steps;

/// <summary>
/// Represents a step in a Given-When-Then (GWT) scenario.
/// </summary>
/// <typeparam name="T">The type of object to use in the step.</typeparam>
public abstract class Step<T>  where T : class
{
    /// <summary>
    /// Gets the value of the object used in the step.
    /// </summary>
    protected T Value { get; }

    /// <summary>
    /// Gets the feature associated with the step.
    /// </summary>
    public Feature<T> Feature { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Step{T}"/> class with the specified feature.
    /// </summary>
    /// <param name="feature">The feature associated with the step.</param>
    protected Step(Feature<T> feature)
    {
        Feature = feature;
        Value = feature.Scenario.Context;
    }
}
