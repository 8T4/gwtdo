using System;
using Gwtdo.Linguistic;

namespace Gwtdo.Scenarios;

/// <summary>
/// Scenario
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed partial class Scenario<T> where T : IFeatureContext
{
    public T Context { get; }
    public ScenarioVariables Let { get; }

    private string Description { get; set; }
    internal Paradigm<T> Paradigms { get; }
    internal Paradigm<T> MappedParadigms { get; }
    public Action<string> RedirectStandardOutput { get; set; }

    public Scenario<T> this[string description]
    {
        set => value.Description = description;
    }

    public Scenario(string description, T context)
    {
        Paradigms = description;
        MappedParadigms = description;
        Context = context;
        Let = new ScenarioVariables();
    }

    public static implicit operator Scenario<T>(Feature<T> feature)
    {
        return feature.SCENARIO;
    }
}

