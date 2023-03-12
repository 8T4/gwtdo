using System;
using Gwtdo.Scenarios;
using Gwtdo.Steps;

namespace Gwtdo;

/// <summary>
/// Although Given-When-Then style is symptomatic to BDD, the basic idea is pretty common when writing tests or
/// specification by example. Meszaros describes the pattern as Four-Phase Test. His four phases are Setup (Given),
/// Exercise (When), Verify (Then) and Teardown [5]. Bill Wake came up with the formulation as Arrange, Act, Assert.
/// <see href="https://martinfowler.com/bliki/GivenWhenThen.html"/>
/// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
/// </summary>
/// <typeparam name="TContext"></typeparam>
public abstract partial class Feature<TContext> where TContext : IFeatureContext
{
    public string Id => Guid.NewGuid().ToString("N");
    
    protected Describe<TContext> DESCRIBE => Describe<TContext>.Create(this);
    protected Arrange<TContext> GIVEN => Arrange<TContext>.Create(Context);
    protected Act<TContext> WHEN => Act<TContext>.Create(Context);
    protected Assert<TContext> THEN => Assert<TContext>.Create(Context);
    protected And AND => And.Create();
    protected ScenarioVariables Let => SCENARIO.Let;

    protected TContext Context { get; }
    public Scenario<TContext> SCENARIO { get; }

    protected Feature()
    {
    }

    protected Feature(TContext context) : this()
    {
        Context = context;
        SCENARIO = new Scenario<TContext>(string.Empty, context);
    }
}