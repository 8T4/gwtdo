namespace Gwtdo.Scenarios;

/// <summary>
/// Represents the result of a scenario.
/// </summary>
public sealed class ScenarioResult
{
    /// <summary>
    /// Gets a value indicating whether the scenario succeeded.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets the message associated with the result.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Gets a value indicating whether the scenario failed.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScenarioResult"/> class.
    /// </summary>
    /// <param name="isSuccess">A value indicating whether the scenario succeeded.</param>
    /// <param name="result">The message associated with the result.</param>
    private ScenarioResult(bool isSuccess, string result)
    {
        IsSuccess = isSuccess;
        Message = result;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ScenarioResult"/> class representing a successful scenario with an empty message.
    /// </summary>
    /// <returns>A new instance of the <see cref="ScenarioResult"/> class representing a successful scenario with an empty message.</returns>
    public static ScenarioResult Ok() => new(true, string.Empty);

    /// <summary>
    /// Creates a new instance of the <see cref="ScenarioResult"/> class representing a successful scenario with the specified message.
    /// </summary>
    /// <param name="result">The message associated with the result.</param>
    /// <returns>A new instance of the <see cref="ScenarioResult"/> class representing a successful scenario with the specified message.</returns>
    public static ScenarioResult Ok(string result) => new(true, result);

    /// <summary>
    /// Creates a new instance of the <see cref="ScenarioResult"/> class representing a failed scenario with the specified message.
    /// </summary>
    /// <param name="result">The message associated with the result.</param>
    /// <returns>A new instance of the <see cref="ScenarioResult"/> class representing a failed scenario with the specified message.</returns>
    public static ScenarioResult Fail(string result) => new(false, result);

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return Message;
    }
}
