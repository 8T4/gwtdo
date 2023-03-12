namespace Gwtdo.Extensions;

/// <summary>
/// Console Colors Method
/// </summary>
internal static class Colors
{
    internal static string Error(string value) =>
        $"\\u001b[0m\\u001b[31m{value}\\u001b[0m";

    internal static string Success(string value) =>
        $"\\u001b[0m\\u001b[32m{value}\\u001b[0m";

    internal static string Warning(string value) =>
        $"\\u001b[0m\\u001b[33m{value}\\u001b[0m";

    internal static string Reset(string value) =>
        $"\\u001b[0m{value}\\u001b[0m";
}