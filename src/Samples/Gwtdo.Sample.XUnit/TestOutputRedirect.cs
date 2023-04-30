using Gwtdo.Console;
using Xunit.Abstractions;

namespace Gwtdo.Sample.XUnit;

public class TestOutputRedirect: IOutputRedirect
{
    private readonly ITestOutputHelper _outputHelper;

    public TestOutputRedirect(ITestOutputHelper outputHelper) =>
        _outputHelper = outputHelper;

    void IOutputRedirect.WriteLine(string message) => 
        _outputHelper.WriteLine(message);

    public void WriteLine(string format, params object[] args) =>
        _outputHelper.WriteLine(format, args);
}