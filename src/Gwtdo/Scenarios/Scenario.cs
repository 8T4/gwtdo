using System;
using Gwtdo.Output;
using Gwtdo.Scenarios.Linguistic;
using Gwtdo.Steps;

namespace Gwtdo.Scenarios;

/// <summary>
/// The Scenario<TContext> class represents a scenario that can be executed as part of a behavior-driven development
/// (BDD) process. It is a generic class, parameterized on the type TContext which represents the context in which the
/// scenario will be executed. 
/// </summary>
/// <typeparam name="TContext"></typeparam>
public sealed partial class Scenario<TContext> where TContext: class
{
    /// <summary>
    /// Context: A read-only property that returns the context in which the scenario will be executed.
    /// </summary>
    public TContext Context { get; }
    /// <summary>
    /// Let: A property of type ScenarioVariables that can be used to define variables
    /// that can be used in the scenario.
    /// </summary>
    public ScenarioVariables Let { get; }
    /// <summary>
    /// Description: An internal property that represents the description of the scenario.
    /// </summary>
    internal string Description { get; set; }
    /// <summary>
    /// Paradigms: An internal property of type Paradigm<TContext> that represents the collection of
    /// paradigms associated with the scenario.
    /// </summary>
    internal Paradigm<TContext> Paradigms { get; }
    /// <summary>
    /// MappedParadigms: An internal property of type Paradigm<TContext> that represents the collection of
    /// mapped paradigms associated with the scenario.
    /// </summary>
    internal Paradigm<TContext> MappedParadigms { get; }
    /// <summary>
    /// OutputRedirect: An internal property of type IOutputRedirect that can be used to redirect the
    /// scenario's output to a custom output target.
    /// </summary>
    internal IOutputRedirect OutputRedirect { get; set; }

    /// <summary>
    /// this[string description]: An indexer that can be used to set the description of the scenario.
    /// </summary>
    /// <param name="description"></param>
    public Scenario<TContext> this[string description]
    {
        set => value.Description = description;
    }

    /// <summary>
    /// Scenario(string description, TContext context, IOutputRedirect? console = null): The constructor of the class,
    /// which takes a description of the scenario, the context in which the scenario will be executed, and an optional
    /// IOutputRedirect instance that can be used to redirect the scenario's output to a custom output target.
    /// </summary>
    /// <param name="description"></param>
    /// <param name="context"></param>
    /// <param name="console"></param>
    public Scenario(string description, TContext context, IOutputRedirect? console = null)
    {
        Context = context;
        Description = description;
        Let = new ScenarioVariables();
        Paradigms = new Paradigm<TContext>();
        MappedParadigms = new Paradigm<TContext>();
        OutputRedirect = console ?? new OutputRedirect();
    }

    /// <summary>
    /// GetDefault(TContext context): A static method that returns a new Scenario<TContext>
    /// instance with an empty description and the given context.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static Scenario<TContext> GetDefault(TContext context) => new(string.Empty, context);
    
    /// <summary>
    /// implicit operator Scenario<TContext>(Feature<TContext> feature): An implicit conversion operator that allows a
    /// Feature<TContext> instance to be implicitly converted to a Scenario<TContext> instance.
    /// </summary>
    /// <param name="feature"></param>
    /// <returns></returns>
    public static implicit operator Scenario<TContext>(Feature<TContext> feature) => feature.Scenario;
    
    /// <summary>
    /// implicit operator Scenario<TContext>(Arrange<TContext> arrange): An implicit conversion operator that allows an
    /// Arrange<TContext> instance to be implicitly converted to a Scenario<TContext> instance.
    /// </summary>
    /// <param name="arrange"></param>
    /// <returns></returns>
    public static implicit operator Scenario<TContext>(Arrange<TContext> arrange) => arrange.Feature.Scenario;
}

