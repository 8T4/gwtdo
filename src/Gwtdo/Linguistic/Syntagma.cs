using System;

namespace Gwtdo.Linguistic
{
    internal readonly struct Syntagma<T>
    {
        public Metalanguage Metalanguage { get; }
        public Sign<Action<T>> Sign { get; }

        public Syntagma(string signifier, Action<T> signified)
        {
            Metalanguage = signifier;
            Sign = new Sign<Action<T>>(signifier, signified);
        }
    }
}