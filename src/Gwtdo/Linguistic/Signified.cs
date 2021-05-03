using System;
using System.Collections.Generic;

namespace Gwtdo.Linguistic
{
    public readonly struct Signified<T>: IEquatable<Signified<T>>
    {
        public T Value { get; }

        private Signified(T value)
        {
            Value = value;
        }
        
        public static implicit operator Signified<T>(T value)
        {
            return new Signified<T>(value);
        }

        public bool Equals(Signified<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is Signified<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }
    }
}