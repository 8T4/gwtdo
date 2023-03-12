using System.Collections.Generic;
using System.Linq;

namespace Gwtdo.Linguistic;

internal record Paradigm<T>
{
    public Metalanguage Metalanguage { get; }
    public Dictionary<string, Syntagma<T>> Syntagmas { get; }

    public bool IsEmpty => !Syntagmas.Any();
    public bool IsNotEmpty => Syntagmas.Any();

    private Paradigm(string description)
    {
        Metalanguage = description;
        Syntagmas = new Dictionary<string, Syntagma<T>>();
    }

    public bool SyntagmaExists(Syntagma<T> syntagma) =>
        Syntagmas.ContainsKey(syntagma.Metalanguage.Sign.Signified.Value);

    public Syntagma<T> GetSyntagma(string syntagma) =>
        Syntagmas[syntagma];

    public void AddSyntagma(Syntagma<T> syntagma) =>
        Syntagmas[syntagma.Metalanguage.Sign.Signified.Value] = syntagma;

    public void Clear() => Syntagmas.Clear();

    public static implicit operator Paradigm<T>(string value)
    {
        return new Paradigm<T>(value);
    }
}