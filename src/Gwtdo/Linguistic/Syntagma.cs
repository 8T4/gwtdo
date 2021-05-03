using System;

namespace Gwtdo.Linguistic
{
    internal readonly struct Syntagma<T> : IEquatable<Syntagma<T>>
    {
        public Metalanguage Metalanguage { get; }
        public Sign<Action<T>> Sign { get; }

        public Syntagma(string signifier, Action<T> signified)
        {
            Metalanguage = signifier;
            Sign = new Sign<Action<T>>(signifier, signified);
        }

        public bool Equals(Syntagma<T> other)
        {
            return Metalanguage.Equals(other.Metalanguage) && Sign.Equals(other.Sign);
        }

        public override bool Equals(object obj)
        {
            return obj is Syntagma<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Metalanguage, Sign);
        }
    }
}