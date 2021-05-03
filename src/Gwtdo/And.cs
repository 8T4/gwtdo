namespace Gwtdo
{
    public sealed class And
    {
        private static And _and;
        public static And Create() => _and ??= new And();
        public static string Name => "\u001b[36;1mAND\u001b[0m";
    }
}