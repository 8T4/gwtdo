using System;

namespace Gwtdo.Extensions
{
    /// <summary>
    /// Methods to use with Console class 
    /// </summary>
    public static class ConsoleMethods
    {
        internal static void PrintLine(int size)
        {
            var line = string.Empty.PadLeft(size, '-');
            Console.WriteLine(line);
        }        
    }
}