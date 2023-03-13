using System;
using Gwtdo.Linguistic;

namespace Gwtdo.Scenarios;

/// <summary>
/// Scenario
/// </summary>
/// <typeparam name="TContext"></typeparam>
public sealed partial class Scenario<TContext> where TContext : IFeatureContext
{
    public TContext Context { get; }
    public ScenarioVariables Let { get; }

    private string Description { get; set; }
    internal Paradigm<TContext> Paradigms { get; }
    internal Paradigm<TContext> MappedParadigms { get; }
    public Action<string> RedirectStandardOutput { get; set; }

    public Scenario<TContext> this[string description]
    {
        set => value.Description = description;
    }

    public Scenario(string description, TContext context)
    {
        Paradigms = description;
        MappedParadigms = description;
        Context = context;
        Let = new ScenarioVariables();
    }

    public static implicit operator Scenario<TContext>(Feature<TContext> feature)
    {
        return feature.SCENARIO;
    }
}

