using System;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Gwtdo.Console;
using Gwtdo.Extensions;

namespace Gwtdo.Scenarios;

/// <summary>
/// Handle Describe for validating and execution
/// </summary>
/// <typeparam name="TContext"></typeparam>
public partial class Scenario<TContext> where TContext : class
{
    public ScenarioResult Execute()
    {
        var result = VerifyIfMappedParadigmsIsNotEmpty();
        result = result.IsFailure ? result : VerifyIfAllMappedScenarios();
        result = result.IsFailure ? result : ExecuteMappedParadigms();
        
        PrintScenarioResult(result);
        return result;
    }
    
    public Task<ScenarioResult> ExecuteAsync()
    {
        var result = VerifyIfMappedParadigmsIsNotEmpty();
        result = result.IsFailure ? result : VerifyIfAllMappedScenarios();
        result = result.IsFailure ? result : ExecuteMappedParadigms();
        
        PrintScenarioResult(result);
        return Task.FromResult(result);
    }    
    
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

    private void AppendScenarioDescription(ref StringBuilder builder)
    {
        builder.AppendHorizontalLine(60);
        builder.AppendLine(Description.ToUpper(CultureInfo.InvariantCulture).Success());
    }

    private void PrintScenarioResult(ScenarioResult scenarioResult)
    {
        var result = Let.Replace(scenarioResult.ToString());
        OutputRedirect.WriteLine(result);
        Paradigms.Clear();
        MappedParadigms.Clear();
    }

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