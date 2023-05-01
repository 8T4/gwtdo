using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Gwtdo.Scenarios.Linguistic;

namespace Gwtdo.Scenarios;

/// <summary>
/// Use this class to Map Describe Expression To Actions
/// </summary>
/// <typeparam name="TContext"></typeparam>
public abstract class ScenarioFixture<TContext> where TContext : class
{
    /// <summary>
    /// Gets or sets the context for the scenario.
    /// </summary>
    protected TContext? Context { get; set; }

    /// <summary>
    /// Gets or sets the scenario for the fixture.
    /// </summary>
    private Scenario<TContext>? Scenario { get; set; }

    /// <summary>
    /// Gets the scenario variables for the fixture.
    /// </summary>
    protected ScenarioVariables Let => Scenario?.Let ?? new ScenarioVariables();

    /// <summary>
    /// Sets the scenario for the fixture.
    /// </summary>
    /// <param name="scenario">The scenario to set.</param>
    public void SetScenario(Scenario<TContext> scenario)
    {
        Scenario = scenario;
        Context = scenario.Context;
    }

    /// <summary>
    /// Maps the scenario methods to the scenario.
    /// </summary>
    public void MapScenario()
    {
        if (Scenario is null)
            throw new FeatureException("Scenario is not defined");

        MapScenarioMethods();
    }

    /// <summary>
    /// Maps the scenario methods to the scenario's mapped paradigms.
    /// </summary>
    private void MapScenarioMethods()
    {
        var methods = GetScenarioMethods();
        if (!methods.Any()) return;

        foreach (var method in methods)
        {
            foreach (var attribute in method.Attributes)
            {
                var syntagma = new Syntagma<TContext>(
                    signifier: ((IGwtCustomAttribute)attribute).Description,
                    signified: _ => method.Info.Invoke(this, new object[] { }));

                Scenario!.MappedParadigms.AddSyntagma(syntagma);
            }
        }
    }

    /// <summary>
    /// Returns a list of all scenario methods decorated with the <see cref="IGwtCustomAttribute"/> attribute.
    /// </summary>
    /// <returns>A list of tuples representing the scenario methods and their attributes.</returns>
    private IEnumerable<(object[] Attributes, MethodInfo Info)> GetScenarioMethods()
    {
        var methods =
            from method in GetType().GetMethods()
            where method.GetCustomAttributes(typeof(IGwtCustomAttribute), false).Any()
            select new
            {
                attributes = method.GetCustomAttributes(typeof(IGwtCustomAttribute), false),
                action = method
            };

        return methods.Select(x => (x.attributes, x.action));
    }
}