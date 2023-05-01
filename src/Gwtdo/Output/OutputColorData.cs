using System;
using System.Collections.Generic;

namespace Gwtdo.Output;

/// <summary>
/// Class based on OutputColorMethods.Net library
/// <exception href="https://github.com/ahmelsayed/Colors.Net/blob/master/LICENSE"></exception>
/// </summary>
internal static class OutputColorData
{
    internal static readonly IDictionary<ConsoleColor, char> ConsoleColorToUnicode = new Dictionary<ConsoleColor, char>
    {
        { ConsoleColor.Black, '\uE000' },
        { ConsoleColor.DarkBlue, '\uE001' },
        { ConsoleColor.DarkGreen, '\uE002' },
        { ConsoleColor.DarkCyan, '\uE003' },
        { ConsoleColor.DarkRed, '\uE004' },
        { ConsoleColor.DarkMagenta, '\uE005' },
        { ConsoleColor.DarkYellow, '\uE006' },
        { ConsoleColor.Gray, '\uE007' },
        { ConsoleColor.DarkGray, '\uE008' },
        { ConsoleColor.Blue, '\uE009' },
        { ConsoleColor.Green, '\uE00A' },
        { ConsoleColor.Cyan, '\uE00B' },
        { ConsoleColor.Red, '\uE00C' },
        { ConsoleColor.Magenta, '\uE00D' },
        { ConsoleColor.Yellow, '\uE00E' },
        { ConsoleColor.White, '\uE00F' }
    };
}