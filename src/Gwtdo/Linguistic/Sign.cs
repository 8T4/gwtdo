using System;

namespace Gwtdo.Linguistic
{
    internal readonly struct Sign<T> : IEquatable<Sign<T>>
    {
        public Signifier Signifier { get; }
        public Signified<T> Signified { get; }

        public Sign(string signifier, T signified)
        {
            Signifier = signifier;
            Signified = signified;
        }

        public bool Equals(Sign<T> other)
        {
            return Signifier.Equals(other.Signifier) && Signified.Equals(other.Signified);
        }

        public override bool Equals(object obj)
        {
            return obj is Sign<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Signifier, Signified);
        }
    }
}