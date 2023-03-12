using System;
using System.Globalization;
using System.Text;
using Gwtdo.Extensions;

namespace Gwtdo.Scenarios;

/// <summary>
/// Handle Scenario for validating and execution
/// </summary>
/// <typeparam name="T"></typeparam>
public partial class Scenario<T> where T : IFeatureContext
{
    public ScenarioResult Execute()
    {
        var mappedParadigmsEmpty = VerifyIfMappedParadigmsIsNotEmpty();
        if (mappedParadigmsEmpty.IsFailure)
        {
            PrintScenarioResult(mappedParadigmsEmpty);
            return mappedParadigmsEmpty;
        }

        var allScenarioWereMapped = VerifyIfAllScenarioWereMapped();
        if (allScenarioWereMapped.IsFailure)
        {
            PrintScenarioResult(allScenarioWereMapped);
            return allScenarioWereMapped;
        }

        var scenarioResult = ExecuteMappedParadigms();
        PrintScenarioResult(scenarioResult);
        return scenarioResult;
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
            Colors.Warning(
                $"the SCENARIO \"{Description.ToUpper(CultureInfo.InvariantCulture)}\" SHOULD BE MAPPED"));
        return ScenarioResult.Fail(result.ToString());
    }

    private ScenarioResult VerifyIfAllScenarioWereMapped()
    {
        var allExpressionMapped = true;

        var result = new StringBuilder();
        AppendScenarioDescription(ref result);

        foreach (var (key, value) in Paradigms.Syntagmas)
        {
            if (!MappedParadigms.SyntagmaExists(value))
            {
                allExpressionMapped = false;
                result.AppendLine(
                    $"{Colors.Reset(value.Metalanguage.Sign.Signifier.Value)} {Colors.Error("(NOT MAPPED)")}"
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
        builder.AppendLine(Colors.Success(Description.ToUpper(CultureInfo.InvariantCulture)));
    }
    
    private void PrintScenarioResult(ScenarioResult scenarioResult)
    {
        var result = Let.Replace(scenarioResult.ToString());
        RedirectStandardOutput?.Invoke(result);
        Paradigms.Clear();
        MappedParadigms.Clear();
    }

    private ScenarioResult ExecuteMappedParadigms()
    {
        var result = new StringBuilder();
        result.AppendLine();
        result.AppendHorizontalLine(60);

        foreach (var (key, value) in Paradigms.Syntagmas)
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
                result.AppendLine($"{mapped.Metalanguage.Sign.Signifier.Value}".Indent(4));
            }
            catch (Exception ex)
            {
                result.AppendLine(Colors.Error(value.Metalanguage.Sign.Signifier.Value.Indent(4)));
                
                result.AppendHorizontalLine(60);
                result.AppendLine(Colors.Warning(ex.Message.Indent(4)));
                if(ex.InnerException is not null)
                    result.AppendLine(Colors.Warning(ex.InnerException.Message.Indent(4)));
                result.AppendHorizontalLine(60);

                throw;

                //return ScenarioResult.Fail(result.ToString());
            }
        }

        result.Insert(0, Colors.Success(Description.ToUpper(CultureInfo.InvariantCulture)));
        return ScenarioResult.Ok(result.ToString());
    }
}