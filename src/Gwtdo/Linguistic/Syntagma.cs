using System;

namespace Gwtdo.Linguistic
{
    internal struct Syntagma<T>
    {
        public Metalanguage Metalanguage { get; private set; }
        public Sign<Action<T>> Sign { get; private set; }

        public Syntagma(string signifier, Action<T> signified)
        {
            Metalanguage = signifier;
            Sign = new Sign<Action<T>>(signifier, signified);
        }
    }
}