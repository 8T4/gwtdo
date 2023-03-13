namespace Gwtdo.Constants;

public struct GwtConstants
{
    const string PREFIX = @"\u001b[36;1m";
    const string SUFIX = @"\u001b[0m";

    public const string WHEN = $@"{PREFIX}WHEN{SUFIX}";
    public const string GIVEN = $@"{PREFIX}GIVEN{SUFIX}";
    public const string THEN = $@"{PREFIX}THEN{SUFIX}";
}