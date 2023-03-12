namespace Gwtdo.Scenarios;

public class ScenarioResult
{
    public bool IsSuccess { get; }
    public string Message { get; }
    public bool IsFailure => !IsSuccess;

    private ScenarioResult(bool isSuccess, string result)
    {
        IsSuccess = isSuccess;
        Message = result;
    }

    public static ScenarioResult Ok() => new(true, string.Empty);
    public static ScenarioResult Ok(string result) => new(true, result);
    public static ScenarioResult Fail(string result) => new(false, result);

    public override string ToString()
    {
        return Message;
    }
}