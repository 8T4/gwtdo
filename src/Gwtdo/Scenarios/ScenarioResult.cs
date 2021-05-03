using System;

namespace Gwtdo.Scenarios
{
    public class ScenarioResult
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public bool IsFailure => !IsSuccess;
        public Exception Exception { get; private set; }

        public ScenarioResult(bool isSuccess, string result, Exception exception)
        {
            IsSuccess = isSuccess;
            Message = result;
            Exception = exception;
        }

        public static ScenarioResult Ok(string result) => new ScenarioResult(true, result, null);
        public static ScenarioResult Fail(string result, Exception e) => new ScenarioResult(false, result, e);
    }
}