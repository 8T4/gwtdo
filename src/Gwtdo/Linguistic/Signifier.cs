namespace Gwtdo.Linguistic;

internal record Signifier
{
    public string Value { get; }

    private Signifier(string value) => Value = value;
    public static implicit operator Signifier(string value) => new(value);
}