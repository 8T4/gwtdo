namespace Gwtdo.Scenarios.Linguistic;

/// <summary>
/// Represents a signifier associated with a sign.
/// </summary>
internal record Signifier
{
    /// <summary>
    /// Gets the value of the signifier.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Signifier"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value of the signifier.</param>
    private Signifier(string value) => Value = value;

    /// <summary>
    /// Implicitly converts a <see cref="string"/> value to a <see cref="Signifier"/> instance.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value to convert to a signifier.</param>
    public static implicit operator Signifier(string value) => new(value);
}
