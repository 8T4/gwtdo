using System;

namespace Gwtdo.Linguistic
{
    internal readonly struct Signifier : IEquatable<Signifier>
    {
        public string Value { get; }

        private Signifier(string value)
            => Value = value;

        public static implicit operator Signifier(string value)
            => new Signifier(value);

        public bool Equals(Signifier other)
            => Value == other.Value;

        public override bool Equals(object obj)
            => obj is Signifier other && Equals(other);

        public override int GetHashCode()
            => (Value != null ? Value.GetHashCode() : 0);
    }
}