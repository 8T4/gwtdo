using System;

namespace Gwtdo.Linguistic
{
    internal readonly struct Sign<T> : IEquatable<Sign<T>>
    {
        public Signifier Signifier { get; }
        public Signified<T> Signified { get; }

        public Sign(string signifier, T signified)
            => (Signifier, Signified) = (signifier, signified);

        public bool Equals(Sign<T> other)
            => Signifier.Equals(other.Signifier) && Signified.Equals(other.Signified);

        public override bool Equals(object obj)
            => obj is Sign<T> other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(Signifier, Signified);
    }
}