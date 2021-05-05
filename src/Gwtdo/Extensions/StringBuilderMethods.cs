using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gwtdo.Extensions
{
    public static class StringBuilderMethods
    {
        internal static void AppendHorizontalLine(this StringBuilder builder, int size)
        {
            var line = string.Empty.PadLeft(size, '-');
            builder.AppendLine(line);
        }
    }
}