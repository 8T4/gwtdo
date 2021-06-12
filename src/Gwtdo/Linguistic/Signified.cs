using System;
using System.Collections.Generic;

namespace Gwtdo.Linguistic
{
    internal readonly struct Signified<T> : IEquatable<Signified<T>>
    {
        public T Value { get; }

        private Signified(T value)
            => Value = value;

        public static implicit operator Signified<T>(T value)
            => new Signified<T>(value);

        public bool Equals(Signified<T> other)
            => EqualityComparer<T>.Default.Equals(Value, other.Value);

        public override bool Equals(object obj)
            => obj is Signified<T> other && Equals(other);

        public override int GetHashCode()
            => EqualityComparer<T>.Default.GetHashCode(Value);
    }
}