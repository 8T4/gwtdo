using Gwtdo.Output;
using Gwtdo.Scenarios.Linguistic;
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
    public static Feature<TContext> operator |(Feature<TContext> feature, Arrange<TContext> other) 
        => GwtStatements(feature, OutputConstants.GIVEN);
    public static Feature<TContext> operator |(Feature<TContext> feature, Act<TContext> other) 
        => GwtStatements(feature, OutputConstants.WHEN);
    public static Feature<TContext> operator |(Feature<TContext> feature, Assert<TContext> other) 
        => GwtStatements(feature, OutputConstants.THEN);

    /// <summary>
    /// Adds the given-when-then statement to the scenario paradigms.
    /// </summary>
    /// <param name="feature">The feature under test.</param>
    /// <param name="value">The given-when-then statement to be added to the scenario paradigms.</param>
    private static Feature<TContext> GwtStatements(Feature<TContext> feature, string value)
    {
        var syntagma = new Syntagma<TContext>(value, null);
        feature.Scenario.Paradigms.AddSyntagma(syntagma);
        feature.Scenario.MappedParadigms.AddSyntagma(syntagma);
        return feature;
    }
}