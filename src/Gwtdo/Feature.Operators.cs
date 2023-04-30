using Gwtdo.Constants;
using Gwtdo.Linguistic;
using Gwtdo.Scenarios;
using Gwtdo.Steps;

namespace Gwtdo;

/// <summary>
/// Feature OPERATORS
/// </summary>
/// <typeparam name="TContext"></typeparam>
public abstract partial class Feature<TContext> where TContext : class
{
    public static Feature<TContext> operator |(Feature<TContext> feature, string other)
    {
        var syntagma = new Syntagma<TContext>(other, null);
    
        if (!feature.Scenario.Paradigms.SyntagmaExists(syntagma))
        {
            feature.Scenario.Paradigms.AddSyntagma(syntagma);
        }

        return feature;
    }
    
    public static Feature<TContext> operator |(Feature<TContext> feature, string[] others)
    {
        foreach (var other in others)
        {
            var syntagma = new Syntagma<TContext>(other, null);

            if (!feature.Scenario.Paradigms.SyntagmaExists(syntagma))
            {
                feature.Scenario.Paradigms.AddSyntagma(syntagma);
            }
        }

        return feature;
    }    

    public static Feature<TContext> operator |(Feature<TContext> feature, And other) 
        => feature;
    public static Feature<TContext> operator |(Feature<TContext> feature, ScenarioVariables other) 
        => feature;
    public static Feature<TContext> operator |(Feature<TContext> feature, Arrange<TContext> other) 
        => GwtStatements(feature, GwtConstants.GIVEN);
    public static Feature<TContext> operator |(Feature<TContext> feature, Act<TContext> other) 
        => GwtStatements(feature, GwtConstants.WHEN);
    public static Feature<TContext> operator |(Feature<TContext> feature, Assert<TContext> other) 
        => GwtStatements(feature, GwtConstants.THEN);

    /// <summary>
    /// Sign default statements GWT in SCENARIO to satisfy mapping methods when It is processed.
    /// </summary>
    /// <param name="feature"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private static Feature<TContext> GwtStatements(Feature<TContext> feature, string value)
    {
        var syntagma = new Syntagma<TContext>(value, null);
        feature.Scenario.Paradigms.AddSyntagma(syntagma);
        feature.Scenario.MappedParadigms.AddSyntagma(syntagma);
        return feature;
    }
}