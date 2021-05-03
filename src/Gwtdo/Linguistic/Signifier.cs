namespace Gwtdo.Linguistic
{
    internal readonly struct Signifier
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
    }
}