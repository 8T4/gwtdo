using System.Collections.Generic;
using System.Linq;

namespace Gwtdo.Linguistic;

internal record Paradigm<T>
{
    public IDictionary<string, Syntagma<T>> SyntagmaCollection { get; } = new Dictionary<string, Syntagma<T>>();

    public bool IsEmpty => !SyntagmaCollection.Any();
    public bool IsNotEmpty => SyntagmaCollection.Any();

    public bool SyntagmaExists(Syntagma<T> syntagma) =>
        SyntagmaCollection.ContainsKey(syntagma.Metalanguage.Sign.Signified.Value!);

    public Syntagma<T> GetSyntagma(string syntagma) =>
        SyntagmaCollection[syntagma];

    public void AddSyntagma(Syntagma<T> syntagma) =>
        SyntagmaCollection[syntagma.Metalanguage.Sign.Signified.Value!] = syntagma;

    public void Clear() => SyntagmaCollection.Clear();
}