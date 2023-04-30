using System;
using Gwtdo.Constants;
using Gwtdo.Linguistic;

namespace Gwtdo.Steps;

/// <summary>
/// Set up the object to be tested. We may need to surround the object with collaborators.
/// For testing purposes, those collaborators might be test objects (mocks, fakes, etc.) or the real thing.
/// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class Arrange<T> : Step<T> where T : class
{
    public Arrange<T> And => this;

    private Arrange(Feature<T> value) : base(value)
    {
    }

    public static Arrange<T> Create(Feature<T> value) => new(value);

    /// <summary>
    /// Use setup to initialize actions that are eager loaded to test your specs.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public Arrange<T> Setup(Action<T> action)
    {
        action.Invoke(Feature.Scenario.Context);
        return this;
    }

    public static Feature<T> operator |(Arrange<T> arrange, string other)
    {
        GwtStatements(arrange.Feature, GwtConstants.GIVEN);

        var syntagma = new Syntagma<T>(other, null);
        if (!arrange.Feature.Scenario.Paradigms.SyntagmaExists(syntagma))
        {
            arrange.Feature.Scenario.Paradigms.AddSyntagma(syntagma);
        }

        return arrange.Feature;
    }
    
    public static Feature<T> operator |(Arrange<T> arrange, string[] others)
    {
        foreach (var other in others)
        {
            var syntagma = new Syntagma<T>(other, null);

            if (!arrange.Feature.Scenario.Paradigms.SyntagmaExists(syntagma))
            {
                arrange.Feature.Scenario.Paradigms.AddSyntagma(syntagma);
            }
        }

        return arrange.Feature;
    }     

    /// <summary>
    /// Sign default statements GWT in SCENARIO to satisfy mapping methods when It is processed.
    /// </summary>
    /// <param name="feature"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private static void GwtStatements(Feature<T> feature, string value)
    {
        var syntagma = new Syntagma<T>(value, null);
        feature.Scenario.Paradigms.AddSyntagma(syntagma);
        feature.Scenario.MappedParadigms.AddSyntagma(syntagma);
    }
}