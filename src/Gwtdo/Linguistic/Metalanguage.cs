using Gwtdo.Extensions;
using System;

namespace Gwtdo.Linguistic
{
    internal readonly struct Metalanguage : IEquatable<Metalanguage>
    {
        public Sign<string> Sign { get; }

        private Metalanguage(string signifier)
            => Sign = new Sign<string>(signifier, signifier.GenerateSlug());

        public static implicit operator Metalanguage(string value)
            => new Metalanguage(value);

        public bool Equals(Metalanguage other)
            => Sign.Equals(other.Sign);

        public override bool Equals(object obj)
            => obj is Metalanguage other && Equals(other);

        public override int GetHashCode()
            => Sign.GetHashCode();
    }
}