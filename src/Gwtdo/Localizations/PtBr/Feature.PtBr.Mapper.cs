using System;
using Gwtdo.Scenarios;

namespace Gwtdo.Localizations.PtBr;

public abstract class FeaturePtBr<TContext, TFixture> : FeaturePtBr<TContext>
    where TContext : IFeatureContext
    where TFixture : ScenarioFixture<TContext>
{
    protected TFixture Fixture { get; }

    protected FeaturePtBr(TContext context) : base(context)
    {
        Fixture = Activator.CreateInstance<TFixture>();
        Fixture.SetScenario(CENARIO);
        Fixture.MapScenarioMethods();
    }
}