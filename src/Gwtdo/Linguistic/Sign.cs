namespace Gwtdo.Linguistic
{
    internal readonly struct Sign<T>
    {
        public Signifier Signifier { get; }
        public Signified<T> Signified { get; }

        public Sign(string signifier, T signified)
        {
            Signifier = signifier;
            Signified = signified;
        }
    }
}