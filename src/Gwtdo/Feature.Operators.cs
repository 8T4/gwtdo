using Gwtdo.Constants;
using Gwtdo.Linguistic;
using Gwtdo.Scenarios;
using Gwtdo.Steps;

namespace Gwtdo;

/// <summary>
/// Feature OPERATORS
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract partial class Feature<T> where T : IFeatureContext
{
    public static Feature<T> operator |(Feature<T> feature, string other)
    {
        var syntagma = new Syntagma<T>(other, null);
    
        if (!feature.SCENARIO.Paradigms.SyntagmaExists(syntagma))
            feature.SCENARIO.Paradigms.AddSyntagma(syntagma);
    
        return feature;
    }

    public static Feature<T> operator |(Feature<T> feature, And other) => feature;
    public static Feature<T> operator |(Feature<T> feature, ScenarioVariables other) => feature;
    public static Feature<T> operator |(Feature<T> feature, Arrange<T> other) => GwtStatements(feature, GwtConstants.GIVEN);
    public static Feature<T> operator |(Feature<T> feature, Act<T> other) => GwtStatements(feature, GwtConstants.WHEN);
    public static Feature<T> operator |(Feature<T> feature, Assert<T> other) => GwtStatements(feature, GwtConstants.THEN);

    /// <summary>
    /// Sign default statements GWT in SCENARIO to satisfy mapping methods when It is processed.
    /// </summary>
    /// <param name="feature"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private static Feature<T> GwtStatements(Feature<T> feature, string value)
    {
        var syntagma = new Syntagma<T>(value, null);
        feature.SCENARIO.Paradigms.AddSyntagma(syntagma);
        feature.SCENARIO.MappedParadigms.AddSyntagma(syntagma);
        return feature;
    }
}