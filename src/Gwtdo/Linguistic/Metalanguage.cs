using Gwtdo.Extensions;
using Gwtdo.Scenarios;

namespace Gwtdo.Linguistic
{
    internal struct Metalanguage
    {
        public Sign<string> Sign { get; private set; }

        private Metalanguage(string signifier)
        {
            Sign = new Sign<string>(signifier, signifier.GenerateSlug());
        }
        
        public static implicit operator Metalanguage(string value)
        {
            return new Metalanguage(value);
        }        
    }
}