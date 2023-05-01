using System;
using Gwtdo.Output;
using Gwtdo.Scenarios.Linguistic;

namespace Gwtdo.Steps;

/// <summary>
/// Represents an arrangement step in a Given-When-Then test scenario.
/// </summary>
/// <typeparam name="T">The type of the feature to be tested.</typeparam>
public sealed class Arrange<T> : Step<T> where T : class
{
    /// <summary>
    /// Gets the current instance of the <see cref="Arrange{T}"/> class.
    /// </summary>
    public Arrange<T> And => this;

    /// <summary>
    /// Initializes a new instance of the <see cref="Arrange{T}"/> class.
    /// </summary>
    /// <param name="value">The feature to be tested.</param>
    private Arrange(Feature<T> value) : base(value)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Arrange{T}"/> class.
    /// </summary>
    /// <param name="value">The feature to be tested.</param>
    /// <returns>A new instance of the <see cref="Arrange{T}"/> class.</returns>
    public static Arrange<T> Create(Feature<T> value) => new(value);

    /// <summary>
    /// Sets up the initial state of the feature under test.
    /// </summary>
    /// <param name="action">The action that sets up the initial state of the feature under test.</param>
    /// <returns>The current instance of the <see cref="Arrange{T}"/> class.</returns>
    public Arrange<T> Setup(Action<T> action)
    {
        action.Invoke(Feature.Scenario.Context);
        return this;
    }

    /// <summary>
    /// Adds a new syntagma to the scenario paradigms.
    /// </summary>
    /// <param name="arrange">The current instance of the <see cref="Arrange{T}"/> class.</param>
    /// <param name="other">The syntagma to be added to the scenario paradigms.</param>
    /// <returns>The feature to be tested.</returns>
    public static Feature<T> operator |(Arrange<T> arrange, string other)
    {
        GwtStatements(arrange.Feature, OutputConstants.GIVEN);

        var syntagma = new Syntagma<T>(other, null);
        if (!arrange.Feature.Scenario.Paradigms.SyntagmaExists(syntagma))
        {
            arrange.Feature.Scenario.Paradigms.AddSyntagma(syntagma);
        }

        return arrange.Feature;
    }

    /// <summary>
    /// Adds the given-when-then statement to the scenario paradigms.
    /// </summary>
    /// <param name="feature">The feature under test.</param>
    /// <param name="value">The given-when-then statement to be added to the scenario paradigms.</param>
    private static void GwtStatements(Feature<T> feature, string value)
    {
        var syntagma = new Syntagma<T>(value, null);
        feature.Scenario.Paradigms.AddSyntagma(syntagma);
        feature.Scenario.MappedParadigms.AddSyntagma(syntagma);
    }
}
