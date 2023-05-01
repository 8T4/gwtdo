using System;

namespace Gwtdo.Steps;

/// <summary>
/// Represents a step in a Given-When-Then (GWT) scenario that performs an assertion on the state of an object.
/// </summary>
/// <typeparam name="T">The type of object on which to perform the assertion.</typeparam>
public sealed class Assert<T> : Step<T> where T : class
{
    /// <summary>
    /// Gets an instance of the <see cref="Assert{T}"/> class.
    /// </summary>
    public Assert<T> And => this;

    /// <summary>
    /// Initializes a new instance of the <see cref="Assert{T}"/> class with the specified feature value.
    /// </summary>
    /// <param name="value">The feature value.</param>
    private Assert(Feature<T> value) : base(value)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Assert{T}"/> class with the specified feature value.
    /// </summary>
    /// <param name="value">The feature value.</param>
    /// <returns>A new instance of the <see cref="Assert{T}"/> class.</returns>
    public static Assert<T> Create(Feature<T> value) => new(value);

    /// <summary>
    /// Performs an assertion on the state of the object.
    /// </summary>
    /// <param name="action">An action that performs the assertion.</param>
    /// <returns>The current instance of the <see cref="Assert{T}"/> class.</returns>
    public Assert<T> Expect(Action<T> action)
    {
        action.Invoke(Value);
        return this;
    }
}
