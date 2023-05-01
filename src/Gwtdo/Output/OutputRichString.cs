namespace Gwtdo.Output;

/// <summary>
/// Class based on OutputColorMethods.Net library
/// <exception href="https://github.com/ahmelsayed/Colors.Net/blob/master/LICENSE"></exception>
/// </summary>
internal readonly struct OutputRichString
{
    private readonly string _value;

    public OutputRichString(string value)
    {
        _value = value;
    }

    public string Value =>
        _value.Length > 0 && _value[0] > '\uE000' && _value[0] == _value[^1]
            ? _value.Trim(_value[0]) : _value;
}