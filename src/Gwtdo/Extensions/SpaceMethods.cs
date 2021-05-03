namespace Gwtdo.Extensions
{
    /// <summary>
    /// Indentation methods
    /// </summary>
    internal static class SpaceMethods
    {
        /// <summary>
        /// Default indentation tabsize
        /// </summary>
        internal static int TabSize => 4;

        /// <summary>
        /// Ident string using default tabsize
        /// </summary>
        /// <returns>indented text</returns>
        internal static string Indent(this string text)
        {
            return string.Empty.PadLeft(TabSize) + text;
        }

        internal static string PrintLine(int size)
        {
            return string.Empty.PadLeft(size, '-');            
        }

        /// <summary>
        /// Ident string using tabsize
        /// </summary>
        /// <param name="text"></param>
        /// <param name="size">tabsize</param>
        /// <returns>indented text</returns>
        internal static string Indent(this string text, int size)
        {
            return string.Empty.PadLeft(size) + text;
        }
    }
}