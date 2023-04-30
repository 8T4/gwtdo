namespace Gwtdo.Linguistic;

internal record Sign<T>
{
    public Signifier Signifier { get; }
    public Signified<T?> Signified { get; }

    public Sign(string signifier, T? signified)
        => (Signifier, Signified) = (signifier, signified);
}