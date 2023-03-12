using System;
using Gwtdo.Scenarios;

namespace Gwtdo;

public abstract partial class Feature<TContext, TMapper> : Feature<TContext>
    where TContext : IFeatureContext
    where TMapper : ScenarioFixture<TContext>
{
    protected TMapper Mapper { get; }

    protected Feature(TContext context) : base(context)
    {
        Mapper = Activator.CreateInstance<TMapper>();
        Mapper.SetScenario(SCENARIO);
        Mapper.MapScenarioMethods();
        //MapScenarioMethods();
    }
}