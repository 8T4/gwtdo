using System;

namespace Gwtdo.Linguistic;

internal record Syntagma<T>
{
    public Metalanguage Metalanguage { get; }
    public Sign<Action<T>> Sign { get; }

    public Syntagma(string signifier, Action<T>? signified) 
        => (Metalanguage, Sign) = (signifier, new Sign<Action<T>>(signifier, signified));
}