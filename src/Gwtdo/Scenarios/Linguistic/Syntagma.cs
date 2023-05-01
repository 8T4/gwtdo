using System;

namespace Gwtdo.Scenarios.Linguistic;

/// <summary>
/// Represents a syntagma, which is a unit of language that consists of a sign and its associated meaning.
/// </summary>
/// <typeparam name="T">The type of the value associated with the sign.</typeparam>
internal record Syntagma<T>
{
    /// <summary>
    /// Gets the metalanguage associated with the syntagma.
    /// </summary>
    public Metalanguage Metalanguage { get; }

    /// <summary>
    /// Gets the sign associated with the syntagma.
    /// </summary>
    public Sign<Action<T>> Sign { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Syntagma{T}"/> class with the specified signifier and signified value.
    /// </summary>
    /// <param name="signifier">The signifier associated with the syntagma.</param>
    /// <param name="signified">The signified value associated with the syntagma.</param>
    public Syntagma(string signifier, Action<T>? signified) 
        => (Metalanguage, Sign) = (signifier, new Sign<Action<T>>(signifier, signified));
}
