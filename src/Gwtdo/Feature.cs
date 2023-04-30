using System;
using System.Threading.Tasks;
using Gwtdo.Console;
using Gwtdo.Scenarios;
using Gwtdo.Steps;

namespace Gwtdo;

public abstract class Feature<TContext, TFixture> : Feature<TContext>
    where TContext : class
    where TFixture : ScenarioFixture<TContext>
{
    protected Feature(TContext context, TFixture? fixture = null) : base(context)
    {
        Fixture = fixture ?? Activator.CreateInstance<TFixture>();
    }
}

/// <summary>
/// Although Given-When-Then style is symptomatic to BDD, the basic idea is pretty common when writing tests or
/// specification by example. Meszaros describes the pattern as Four-Phase Test. His four phases are Setup (Given),
/// Exercise (When), Verify (Then) and Teardown [5]. Bill Wake came up with the formulation as Arrange, Act, Assert.
/// <see href="https://martinfowler.com/bliki/GivenWhenThen.html"/>
/// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
/// </summary>
/// <typeparam name="TContext"></typeparam>
public abstract partial class Feature<TContext>
{
    [Obsolete("Use 'Describe()' method")] protected Describe<TContext> DESCRIBE => Describe<TContext>.Create(this);
    protected Arrange<TContext> GIVEN => Arrange<TContext>.Create(this);
    protected Act<TContext> WHEN => Act<TContext>.Create(this);
    protected Assert<TContext> THEN => Assert<TContext>.Create(this);
    protected And AND => And.Create();
    protected ScenarioVariables Let => SCENARIO.Let;

    [Obsolete("Use 'Describe' method")] public Scenario<TContext> SCENARIO { get; private set; }
    public Scenario<TContext> Scenario { get; }
    protected TContext FeatureContext { get; }
    protected ScenarioFixture<TContext>? Fixture { get; set; }

    protected Feature(TContext context)
    {
        FeatureContext = context;
        Scenario = Scenario<TContext>.GetDefault(FeatureContext);
        SCENARIO = Scenario;
    }

    protected void SetOutputRedirect(IOutputRedirect outputRedirect)
        => Scenario.OutputRedirect = outputRedirect;

    /// <summary>
    /// Describe your methods
    /// </summary>
    /// <param name="description">describe scenario</param>
    /// <param name="feature"></param>
    protected void Describe(string description, Feature<TContext> feature)
    {
        try
        {
            if (feature.FeatureContext is IFeatureContextLifeCycle ctx)
                ctx.Setup();
            
            feature.Scenario[description] = feature;

            if (feature.Fixture is not null)
            {
                feature.Fixture.SetScenario(feature.Scenario);
                feature.Fixture.MapScenario();
            }

            var result = feature.Scenario.Execute();

            if (result.IsFailure)
                throw new FeatureException($"the feature '{feature.Scenario.Description}' fault!!");
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
            throw;
        }
        finally
        {
            if (feature.FeatureContext is IFeatureContextLifeCycle ctx)
                ctx.TearDown();            
        }
    }
    
    /// <summary>
    /// Describe your methods
    /// </summary>
    /// <param name="description">describe scenario</param>
    /// <param name="feature"></param>
    protected async Task DescribeAsync(string description, Feature<TContext> feature)
    {
        try
        {
            switch (feature.FeatureContext)
            {
                case IFeatureContextAsyncLifeCycle ctx:
                    await ctx.SetupAsync();
                    break;
                case IFeatureContextLifeCycle ctx2:
                    ctx2.Setup();
                    break;
            }

            feature.Scenario[description] = feature;

            if (feature.Fixture is not null)
            {
                feature.Fixture.SetScenario(feature.Scenario);
                feature.Fixture.MapScenario();
            }

            var result = await feature.Scenario.ExecuteAsync();

            if (result.IsFailure)
                throw new FeatureException($"the feature '{feature.Scenario.Description}' fault!!");
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
            throw;
        }
        finally
        {
            switch (feature.FeatureContext)
            {
                case IFeatureContextAsyncLifeCycle ctx:
                    await ctx.TearDownAsync();
                    break;
                case IFeatureContextLifeCycle ctx2:
                    ctx2.TearDown();
                    break;
            }            
        }
    }    
}