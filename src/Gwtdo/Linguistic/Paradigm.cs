using System;
using System.Collections.Generic;

namespace Gwtdo.Linguistic
{
    internal readonly struct Paradigm<T> : IEquatable<Paradigm<T>>
    {
        public Metalanguage Metalanguage { get; }
        public Dictionary<string, Syntagma<T>> Syntagmas { get; }

        private Paradigm(string description)
        {
            Metalanguage = description;
            Syntagmas = new Dictionary<string, Syntagma<T>>();
        }

        public bool SyntagmaExists(Syntagma<T> syntagma) =>
            Syntagmas.ContainsKey(syntagma.Metalanguage.Sign.Signified.Value);    
        
        public Syntagma<T> GetSyntagma(string syntagma) =>
            Syntagmas[syntagma];         
        
        public void AddSyntagma(Syntagma<T> syntagma) =>
            Syntagmas[syntagma.Metalanguage.Sign.Signified.Value] = syntagma;

        public static implicit operator Paradigm<T>(string value)
        {
            return new Paradigm<T>(value);
        }

        public bool Equals(Paradigm<T> other)
        {
            return Equals(Syntagmas, other.Syntagmas);
        }

        public override bool Equals(object obj)
        {
            return obj is Paradigm<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Syntagmas != null ? Syntagmas.GetHashCode() : 0);
        }
    }
}