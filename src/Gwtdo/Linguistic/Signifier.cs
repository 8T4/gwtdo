using System;

namespace Gwtdo.Linguistic
{
    internal readonly struct Signifier : IEquatable<Signifier>
    {
        public string Value { get; }

        private Signifier(string value)
        {
            Value = value;
        }
        
        public static implicit operator Signifier(string value)
        {
            return new Signifier(value);
        }

        public bool Equals(Signifier other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Signifier other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
    }
}