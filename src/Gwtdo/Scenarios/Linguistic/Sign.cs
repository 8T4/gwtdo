namespace Gwtdo.Scenarios.Linguistic;

/// <summary>
/// Represents a sign consisting of a signifier and a signified value.
/// </summary>
/// <typeparam name="T">The type of the signified value.</typeparam>
internal record Sign<T>
{
    /// <summary>
    /// Gets the signifier associated with the sign.
    /// </summary>
    public Signifier Signifier { get; }

    /// <summary>
    /// Gets the signified value associated with the sign.
    /// </summary>
    public Signified<T?> Signified { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Sign{T}"/> class with the specified signifier and signified value.
    /// </summary>
    /// <param name="signifier">The signifier associated with the sign.</param>
    /// <param name="signified">The signified value associated with the sign.</param>
    public Sign(string signifier, T? signified)
        => (Signifier, Signified) = (signifier, signified);
}
