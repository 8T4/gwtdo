using System;
using Gwtdo.Console;
using Gwtdo.Linguistic;
using Gwtdo.Steps;

namespace Gwtdo.Scenarios;

/// <summary>
/// Describe
/// </summary>
/// <typeparam name="TContext"></typeparam>
public sealed partial class Scenario<TContext> where TContext: class
{
    public TContext Context { get; }
    public ScenarioVariables Let { get; }
    internal string Description { get; set; }
    internal Paradigm<TContext> Paradigms { get; }
    internal Paradigm<TContext> MappedParadigms { get; }
    internal IOutputRedirect OutputRedirect { get; set; }

    public Scenario<TContext> this[string description]
    {
        set => value.Description = description;
    }

    public Scenario(string description, TContext context, IOutputRedirect? console = null)
    {
        Description = description;
        Context = context;
        Paradigms = new Paradigm<TContext>();
        MappedParadigms = new Paradigm<TContext>();
        Let = new ScenarioVariables();
        OutputRedirect = console ?? new OutputRedirect();
    }

    public static Scenario<TContext> GetDefault(TContext context) => new(string.Empty, context);
    public static implicit operator Scenario<TContext>(Feature<TContext> feature) => feature.Scenario;
    public static implicit operator Scenario<TContext>(Arrange<TContext> arrange) => arrange.Feature.Scenario;
}

