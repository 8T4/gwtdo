using System;
using Gwtdo.Scenarios;

namespace Gwtdo;

public abstract class Feature<TContext, TFixture> : Feature<TContext>
    where TContext : IFeatureContext
    where TFixture : ScenarioFixture<TContext>
{
    protected TFixture Fixture { get; }

    protected Feature(TContext context) : base(context)
    {
        Fixture = Activator.CreateInstance<TFixture>();
        Fixture.SetScenario(SCENARIO);
        Fixture.MapScenarioMethods();
    }
}