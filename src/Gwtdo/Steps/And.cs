namespace Gwtdo.Steps;

public sealed class And
{
    private static And? _and;
    public static And Create() => _and ??= new And();
}