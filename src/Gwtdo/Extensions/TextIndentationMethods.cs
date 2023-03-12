using System.Text;

namespace Gwtdo.Extensions;

/// <summary>
/// Indentation methods
/// </summary>
internal static class SpaceMethods
{
    /// <summary>
    /// Default indentation
    /// </summary>
    private static int TabSize => 4;

    /// <summary>
    /// Ident string
    /// </summary>
    /// <returns>indented text</returns>
    internal static string Indent(this string text) => Indent(text, TabSize);

    /// <summary>
    /// Ident string
    /// </summary>
    /// <param name="text"></param>
    /// <param name="size">size</param>
    /// <returns>indented text</returns>
    internal static string Indent(this string text, int size) => $"{string.Empty.PadLeft(size)}{text}";

    internal static void AppendHorizontalLine(this StringBuilder builder, int size)
    {
        var line = string.Empty.PadLeft(size, '-');
        builder.AppendLine(line);
    }
}