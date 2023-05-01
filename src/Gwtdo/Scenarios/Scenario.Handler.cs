using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Gwtdo.Output;
using Gwtdo.Output.Extensions;

namespace Gwtdo.Scenarios;

/// <summary>
/// Handle Describe for validating and execution
/// </summary>
/// <typeparam name="TContext"></typeparam>
public partial class Scenario<TContext> where TContext : class
{
    /// <summary>
    /// Execute(): a public method that executes the scenario synchronously. It first verifies if all expressions have
    /// been mapped, then verifies if all mapped scenarios are valid, and finally executes all mapped scenarios.
    /// The result of the scenario is returned as a ScenarioResult.
    /// </summary>
    /// <returns></returns>
    public ScenarioResult Execute()
    {
        var result = VerifyIfMappedParadigmsIsNotEmpty();
        result = result.IsFailure ? result : VerifyIfAllMappedScenarios();
        result = result.IsFailure ? result : ExecuteMappedParadigms();
        
        PrintScenarioResult(result);
        return result;
    }
    
    /// <summary>
    /// ExecuteAsync(): a public method that executes the scenario asynchronously. It first verifies if all expressions
    /// have been mapped, then verifies if all mapped scenarios are valid, and finally executes all mapped scenarios.
    /// The result of the scenario is returned as a Task<ScenarioResult>.
    /// </summary>
    /// <returns></returns>
    public Task<ScenarioResult> ExecuteAsync()
    {
        var result = VerifyIfMappedParadigmsIsNotEmpty();
        result = result.IsFailure ? result : VerifyIfAllMappedScenarios();
        result = result.IsFailure ? result : ExecuteMappedParadigms();
        
        PrintScenarioResult(result);
        return Task.FromResult(result);
    }    
    
    /// <summary>
    /// VerifyIfMappedParadigmsIsNotEmpty(): a private method that verifies if there are any mapped expressions in the
    /// scenario. If there are no mapped expressions, it returns a failed ScenarioResult with an appropriate error
    /// message. Otherwise, it returns a successful ScenarioResult.
    /// </summary>
    /// <returns></returns>    
    private ScenarioResult VerifyIfMappedParadigmsIsNotEmpty()
    {
        var result = new StringBuilder();
        AppendScenarioDescription(ref result);

        if (MappedParadigms.IsNotEmpty)
        {
            return ScenarioResult.Ok();
        }

        result.Insert(0,
            $"the SCENARIO \"{Description.ToUpper(CultureInfo.InvariantCulture)}\" SHOULD BE MAPPED".Warning());
        return ScenarioResult.Fail(result.ToString());
    }
    
    /// <summary>
    /// VerifyIfAllMappedScenarios(): a private method that verifies if all mapped scenarios are valid. It checks if
    /// each expression in the scenario has a corresponding mapped action. If an expression is not mapped, it is added
    /// to the error message of the ScenarioResult. If all expressions are mapped, a successful ScenarioResult is returned.
    /// </summary>
    /// <returns></returns>
    private ScenarioResult VerifyIfAllMappedScenarios()
    {
        var allExpressionMapped = true;

        var result = new StringBuilder();
        AppendScenarioDescription(ref result);

        foreach (var (key, value) in Paradigms.SyntagmaCollection)
        {
            if (!MappedParadigms.SyntagmaExists(value))
            {
                allExpressionMapped = false;
                result.AppendLine(
                    $"{value.Metalanguage.Sign.Signifier.Value.Reset()} {"(NOT MAPPED)".Error()}"
                        .Indent());
                continue;
            }

            var mapped = MappedParadigms.GetSyntagma(key);

            if (mapped.Sign.Signified.Value == null)
            {
                result.AppendLine($"{value.Metalanguage.Sign.Signifier.Value}");
                continue;
            }

            result.AppendLine($"{value.Metalanguage.Sign.Signifier.Value}".Indent());
        }

        return allExpressionMapped
            ? ScenarioResult.Ok(result.ToString())
            : ScenarioResult.Fail(result.ToString());
    }

    /// <summary>
    /// AppendScenarioDescription(): a private method that appends the scenario description to a StringBuilder.
    /// </summary>
    /// <param name="builder"></param>
    private void AppendScenarioDescription(ref StringBuilder builder)
    {
        builder.AppendHorizontalLine(60);
        builder.AppendLine(Description.ToUpper(CultureInfo.InvariantCulture).Success());
    }

    /// <summary>
    /// PrintScenarioResult(): a private method that writes the scenario result to the output. It also clears the
    /// Paradigms and MappedParadigms properties of the class.
    /// </summary>
    /// <param name="scenarioResult"></param>
    private void PrintScenarioResult(ScenarioResult scenarioResult)
    {
        var result = Let.Replace(scenarioResult.ToString());
        OutputRedirect.WriteLine(result);
        Paradigms.Clear();
        MappedParadigms.Clear();
    }

    /// <summary>
    /// ExecuteMappedParadigms(): a private method that executes all mapped scenarios in the scenario. It loops through
    /// all expressions in the Paradigms property, gets the corresponding mapped action from the MappedParadigms
    /// property, and executes it. If an exception is thrown during execution, the method returns a failed
    /// ScenarioResult with an appropriate error message. Otherwise, it returns a successful ScenarioResult with the
    /// result of the scenario.
    /// </summary>
    /// <returns></returns>
    private ScenarioResult ExecuteMappedParadigms()
    {
        var result = new StringBuilder();
        result.AppendLine();
        result.AppendHorizontalLine(60);

        foreach (var (key, value) in Paradigms.SyntagmaCollection)
        {
            try
            {
                var mapped = MappedParadigms.GetSyntagma(key);

                if (mapped.Sign.Signified.Value == null)
                {
                    result.AppendLine(mapped.Metalanguage.Sign.Signifier.Value);
                    continue;
                }

                mapped.Sign.Signified.Value.Invoke(Context);
                result.AppendLine(mapped.Metalanguage.Sign.Signifier.Value.Indent(4));
            }
            catch (Exception ex)
            {
                result.AppendLine($"{value.Metalanguage.Sign.Signifier.Value.Indent(4)} << {"Fail".Error()}");
                result.AppendHorizontalLine(60);
                result.AppendLine(ex.Message.Indent(4).Warning());
                
                if (ex.InnerException is not null)
                {
                    result.AppendLine(ex.InnerException.Message.Indent(4).Warning());
                    result.AppendHorizontalLine(60);
                    result.AppendLine(ex.InnerException.StackTrace.Error());
                }

                result.AppendHorizontalLine(60);
                return ScenarioResult.Fail(result.ToString());
            }
        }

        result.Insert(0, Description.ToUpper(CultureInfo.InvariantCulture).Success());
        return ScenarioResult.Ok(result.ToString());
    }
}