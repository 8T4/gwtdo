namespace Gwtdo.Linguistic
{
    public readonly struct Signified<T>
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
    }
}