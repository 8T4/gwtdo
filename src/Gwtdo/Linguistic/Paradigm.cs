using System.Collections.Generic;

namespace Gwtdo.Linguistic
{
    internal readonly struct Paradigm<T>
    {
        public Dictionary<string, Syntagma<T>> Syntagmas { get; }

        private Paradigm(string description)
        {
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
    }
}