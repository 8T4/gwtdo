namespace Gwtdo.Scenarios.Linguistic;

/// <summary>
/// Represents a signified value associated with a sign.
/// </summary>
/// <typeparam name="T">The type of the signified value.</typeparam>
internal record Signified<T>
{
    /// <summary>
    /// Gets the value of the signified value.
    /// </summary>
    public T? Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Signified{T}"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value of the signified value.</param>
    private Signified(T? value) => Value = value;

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="T"/> to a <see cref="Signified{T}"/> instance.
    /// </summary>
    /// <param name="value">The value to convert to a signified value.</param>
    public static implicit operator Signified<T>(T value) => new(value);
}
