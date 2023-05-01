using Gwtdo.Output.Extensions;

namespace Gwtdo.Scenarios.Linguistic;

/// <summary>
/// Represents a metalanguage, consisting of a signifier and an associated slug.
/// </summary>
internal record Metalanguage
{
    /// <summary>
    /// Gets the sign associated with the metalanguage.
    /// </summary>
    public Sign<string> Sign { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Metalanguage"/> class with the specified signifier.
    /// </summary>
    /// <param name="signifier">The signifier associated with the metalanguage.</param>
    private Metalanguage(string signifier) 
        => Sign = new Sign<string>(signifier, signifier.GenerateSlug());

    /// <summary>
    /// Implicitly converts a string to a <see cref="Metalanguage"/> instance with the specified signifier.
    /// </summary>
    /// <param name="value">The value of the signifier for the metalanguage.</param>
    public static implicit operator Metalanguage(string value) => new (value);
}
