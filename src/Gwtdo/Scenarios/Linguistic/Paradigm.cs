using System.Collections.Generic;
using System.Linq;

namespace Gwtdo.Scenarios.Linguistic;

/// <summary>
/// Represents a record for storing and managing syntagmas in a paradigm.
/// </summary>
/// <typeparam name="T">The type of data associated with the syntagmas.</typeparam>
internal record Paradigm<T>
{
    /// <summary>
    /// Gets the collection of syntagmas in the paradigm.
    /// </summary>
    public IDictionary<string, Syntagma<T>> SyntagmaCollection { get; } = new Dictionary<string, Syntagma<T>>();

    /// <summary>
    /// Gets a value indicating whether the collection of syntagmas in the paradigm is empty.
    /// </summary>
    public bool IsEmpty => !SyntagmaCollection.Any();

    /// <summary>
    /// Gets a value indicating whether the collection of syntagmas in the paradigm is not empty.
    /// </summary>
    public bool IsNotEmpty => SyntagmaCollection.Any();

    /// <summary>
    /// Determines whether the specified syntagma exists in the paradigm.
    /// </summary>
    /// <param name="syntagma">The syntagma to search for.</param>
    /// <returns><c>true</c> if the specified syntagma exists in the paradigm; otherwise, <c>false</c>.</returns>
    public bool SyntagmaExists(Syntagma<T> syntagma) =>
        SyntagmaCollection.ContainsKey(syntagma.Metalanguage.Sign.Signified.Value!);

    /// <summary>
    /// Gets the syntagma with the specified name from the paradigm.
    /// </summary>
    /// <param name="syntagma">The name of the syntagma to retrieve.</param>
    /// <returns>The syntagma with the specified name.</returns>
    public Syntagma<T> GetSyntagma(string syntagma) =>
        SyntagmaCollection[syntagma];

    /// <summary>
    /// Adds the specified syntagma to the paradigm.
    /// </summary>
    /// <param name="syntagma">The syntagma to add.</param>
    public void AddSyntagma(Syntagma<T> syntagma) =>
        SyntagmaCollection[syntagma.Metalanguage.Sign.Signified.Value!] = syntagma;

    /// <summary>
    /// Removes all syntagmas from the paradigm.
    /// </summary>
    public void Clear() => SyntagmaCollection.Clear();
}
