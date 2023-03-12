using System;
using Gwtdo.Scenarios;

namespace Gwtdo.Localizations.PtBr;

public abstract class FeaturePtBr<TFixture, TMapper> : FeaturePtBr<TFixture>
    where TFixture : IFeatureContext
    where TMapper : ScenarioFixture<TFixture>
{
    protected TMapper Mapper { get; }

    protected FeaturePtBr(TFixture context) : base(context)
    {
        Mapper = Activator.CreateInstance<TMapper>();
        Mapper.SetScenario(CENARIO);
        Mapper.MapScenarioMethods();
    }
}