namespace Gwtdo.Console;

/// <summary>
/// Class based on OutputColorMethods.Net library
/// <exception href="https://github.com/ahmelsayed/OutputColorMethods.Net/blob/master/LICENSE"></exception>
/// </summary>
internal readonly struct OutputRichString
{
    private readonly string _value;

    public OutputRichString(string value)
    {
        _value = value;
    }

    public override string ToString() => _value;
    public override int GetHashCode() => _value.GetHashCode();
    public override bool Equals(object obj) => ToString().Equals(obj);
    public bool Equals(OutputRichString other) => ToString().Equals(other.ToString());


    public string Value =>
        _value.Length > 0 && _value[0] > '\uE000' && _value[0] == _value[^1]
            ? _value.Trim(_value[0]) : _value;

    public static bool operator ==(OutputRichString rs1, OutputRichString rs2) => rs1.Equals(rs2);
    public static bool operator !=(OutputRichString rs1, OutputRichString rs2) => !rs1.Equals(rs2);
    public static bool operator ==(string str, OutputRichString rs2) => str.Equals(rs2.ToString());
    public static bool operator !=(string str, OutputRichString rs2) => !str.Equals(rs2.ToString());
    public static OutputRichString operator +(OutputRichString rs1, OutputRichString rs2) => new(rs1 + rs2.ToString());
    public static OutputRichString operator +(string str, OutputRichString rs2) => new(str + rs2.ToString());
}