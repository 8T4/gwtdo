using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Gwtdo
{
    public class And
    {
        private static And _and;
        public static And Create() => _and ??= new And();
    }
}