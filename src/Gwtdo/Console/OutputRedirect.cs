namespace Gwtdo.Console;

internal class OutputRedirect: IOutputRedirect
{
    public void WriteLine(string message) => System.Console.WriteLine(message);
    public void WriteLine(string format, params object[] args) => System.Console.WriteLine(format, args);
}