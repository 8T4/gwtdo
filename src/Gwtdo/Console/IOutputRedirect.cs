namespace Gwtdo.Console;

public interface IOutputRedirect
{
    void WriteLine(string message);
    void WriteLine(string format, params object[] args);
}