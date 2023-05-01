using System;
using System.Threading.Tasks;
using Gwtdo.Output;
using Gwtdo.Scenarios;
using Gwtdo.Steps;

namespace Gwtdo;

/// <summary>
/// This is a C# abstract class Feature<TContext, TFixture> which extends Feature<TContext> generic class, where
/// TContext is a generic type parameter representing the context of the feature and TFixture is a generic type
/// parameter representing the fixture to be used in the feature. 
/// </summary>
/// <typeparam name="TContext"></typeparam>
/// <typeparam name="TFixture"></typeparam>
public abstract class Feature<TContext, TFixture> : Feature<TContext>
    where TContext : class
    where TFixture : ScenarioFixture<TContext>
{
    /// <summary>
    /// It is abstract, which means that it cannot be instantiated directly.
    /// 
    /// It includes a constructor that accepts two parameters: context of type TContext and an optional fixture of type
    /// TFixture. The constructor initializes the context using the base constructor and initializes the fixture with
    /// either the provided value or a new instance created with Activator.CreateInstance<TFixture>().
    ///
    /// The class provides access to the Fixture property which is of type TFixture? and may be null.
    ///
    /// The class is generic over two type parameters: TContext and TFixture. TContext represents the context in which
    /// the feature is being executed, and TFixture represents the fixture to be used for the feature.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="fixture"></param>
    protected Feature(TContext context, TFixture? fixture = null) : base(context)
    {
        Fixture = fixture ?? Activator.CreateInstance<TFixture>();
    }
}

/// <summary>
/// Base class for BDD-style tests that involve specifying feature behavior as scenarios.
/// </summary>
/// <typeparam name="TContext">The type of the context object containing the feature behavior.</typeparam>
public abstract partial class Feature<TContext>
{
    /// <summary>
    /// Gets a <see cref="Describe{TContext}"/> instance that can be used to create a scenario description.
    /// </summary>    
    [Obsolete("Use 'Describe()' method")] 
    protected Describe<TContext> DESCRIBE => Describe<TContext>.Create(this);
    /// <summary>
    /// Gets an <see cref="Arrange{T}"/> instance that can be used to configure the context object.
    /// </summary>    
    protected Arrange<TContext> GIVEN => Arrange<TContext>.Create(this);
    /// <summary>
    /// Gets an <see cref="Act{T}"/> instance that can be used to execute the feature behavior.
    /// </summary>    
    protected Act<TContext> WHEN => Act<TContext>.Create(this);
    /// <summary>
    /// Gets an <see cref="Assert{T}"/> instance that can be used to verify the outcome of the feature behavior.
    /// </summary>    
    protected Assert<TContext> THEN => Assert<TContext>.Create(this);
    /// <summary>
    /// Gets an <see cref="And"/> instance that can be used to chain steps in a scenario.
    /// </summary>    
    protected And AND => And.Create();
    /// <summary>
    /// Gets a <see cref="ScenarioVariables"/> instance that can be used to define variables that are shared across steps in a scenario.
    /// </summary>    
    protected ScenarioVariables Let => Scenario.Let;

    [Obsolete("Use 'Describe' method")] 
    public Scenario<TContext> SCENARIO { get; private set; }
    /// <summary>
    /// Gets the <see cref="Scenario{TContext}"/> instance associated with this feature.
    /// </summary>    
    public Scenario<TContext> Scenario { get; }
    /// <summary>
    /// Gets the context object containing the feature behavior.
    /// </summary>
    protected TContext FeatureContext { get; }
    /// <summary>
    /// Gets the <see cref="ScenarioFixture{TContext}"/> instance associated with this feature.
    /// </summary>    
    protected ScenarioFixture<TContext>? Fixture { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Feature{TContext}"/> class with the specified context object.
    /// </summary>
    /// <param name="context">The context object containing the feature behavior.</param>    
    protected Feature(TContext context)
    {
        FeatureContext = context;
        Scenario = Scenario<TContext>.GetDefault(FeatureContext);
        SCENARIO = Scenario;
    }

    /// <summary>
    /// Sets the output redirection for the feature.
    /// </summary>
    /// <param name="outputRedirect">The output redirection object.</param>    
    protected void SetOutputRedirect(IOutputRedirect outputRedirect)
        => Scenario.OutputRedirect = outputRedirect;

    /// <summary>
    /// Describes the behavior of the feature by creating and executing a scenario.
    /// </summary>
    /// <param name="description">The description of the scenario.</param>
    /// <param name="feature">The feature object.</param>
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
    /// Async method to describes the behavior of the feature by creating and executing a scenario.
    /// </summary>
    /// <param name="description">The description of the scenario.</param>
    /// <param name="feature">The feature object.</param>
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