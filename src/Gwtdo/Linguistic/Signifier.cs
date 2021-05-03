namespace Gwtdo.Linguistic
{
    internal struct Signifier
    {
        public string Value { get; set; }

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