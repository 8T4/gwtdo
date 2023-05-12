using System;

namespace Gwtdo.Output;

/// <summary>
/// Console OutputColorMethods Method
/// </summary>
internal static class OutputColorMethods
{
    internal static string Error(this string value) =>
        Write(
            @$"\u001b[0m\u001b[31m{value}\u001b[0m",
            value.ColorString(ConsoleColor.Red).Value
        );

    internal static string Success(this string value) =>
        Write(
            $"\\u001b[32m{value}\\u001b[0m",
            value.ColorString(ConsoleColor.Green).Value
        );

    internal static string Warning(this string value) =>
        Write(
            @$"\u001b[0m\u001b[33m{value}\u001b[0m",
            value.ColorString(ConsoleColor.Yellow).Value
        );

    internal static string Reset(this string value) =>
        Write(
            @$"\u001b[0m\u001b[0m{value}\u001b[0m",
            value.ColorString(ConsoleColor.White).Value
        );

    private static string Write(string colorUnix, string colorWindows)
    {
        return Console.IsOutputRedirected
            ? Environment.OSVersion.Platform switch
            {
                PlatformID.MacOSX => colorUnix,
                PlatformID.Unix => colorUnix,
                _ => colorWindows
            }
            : colorWindows;
    }

    private static OutputRichString ColorString(this string value, ConsoleColor color)
    {
        if (string.IsNullOrEmpty(value)) return new OutputRichString(string.Empty);

        var colorChar = OutputColorData.ConsoleColorToUnicode[color];
        if (value[0] >= '\uE000')
        {
            value = value.Trim(value[0]);
        }

        return new OutputRichString($"{colorChar}{value}{colorChar}");
    }
}