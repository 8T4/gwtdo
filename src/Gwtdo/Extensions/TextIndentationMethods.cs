using System.Text;

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
        private static int TabSize => 4;

        /// <summary>
        /// Ident string using default tabsize
        /// </summary>
        /// <returns>indented text</returns>
        internal static string Indent(this string text)
        {
            return string.Empty.PadLeft(TabSize) + text;
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
        
        internal static void AppendHorizontalLine(this StringBuilder builder, int size)
        {
            var line = string.Empty.PadLeft(size, '-');
            builder.AppendLine(line);
        }          
    }
}