namespace Gwtdo.Linguistic
{
    internal struct Sign<T>
    {
        public Signifier Signifier { get; private set; }
        public Signified<T> Signified { get; private set; }

        public Sign(string signifier, T signified)
        {
            Signifier = signifier;
            Signified = signified;
        }
    }
}