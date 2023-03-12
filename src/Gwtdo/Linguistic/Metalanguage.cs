using Gwtdo.Extensions;

namespace Gwtdo.Linguistic;

internal record Metalanguage
{
    public Sign<string> Sign { get; }

    private Metalanguage(string signifier) 
        => Sign = new Sign<string>(signifier, signifier.GenerateSlug());

    public static implicit operator Metalanguage(string value) => new (value);
}