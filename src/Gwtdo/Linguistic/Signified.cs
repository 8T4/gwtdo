namespace Gwtdo.Linguistic;

internal record Signified<T>
{
    public T Value { get; }

    private Signified(T value) => Value = value;
    public static implicit operator Signified<T>(T value) => new(value);
}