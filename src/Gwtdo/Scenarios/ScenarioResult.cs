namespace Gwtdo.Scenarios
{
    public class ScenarioResult
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public bool IsFailure => !IsSuccess;

        private ScenarioResult(bool isSuccess, string result)
        {
            IsSuccess = isSuccess;
            Message = result;
        }

        public static ScenarioResult Ok(string result) => new ScenarioResult(true, result);
        public static ScenarioResult Fail(string result) => new ScenarioResult(false, result);
    }
}