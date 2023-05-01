namespace Gwtdo.Output;

/// <summary>
/// Defines a contract for redirecting output messages to a specific location.
/// </summary>
public interface IOutputRedirect
{
    /// <summary>
    /// Writes a message to the output location.
    /// </summary>
    /// <param name="message">The message to write.</param>
    void WriteLine(string message);

    /// <summary>
    /// Writes a formatted message to the output location.
    /// </summary>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">An object array that contains zero or more objects to format.</param>
    void WriteLine(string format, params object[] args);
}
