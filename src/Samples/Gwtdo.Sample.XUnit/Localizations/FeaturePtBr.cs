using Gwtdo.Scenarios;
using Gwtdo.Steps;

namespace Gwtdo.Sample.XUnit.Localizations;

public abstract class FeaturePtBr<TContext, TFixture> : Feature<TContext, TFixture>
    where TContext : class
    where TFixture : ScenarioFixture<TContext>
{
    protected Arrange<TContext> DADO => GIVEN;
    protected Act<TContext> QUANDO => WHEN;
    protected Assert<TContext> ENTAO => THEN;
    protected And E => AND;    
    
    protected FeaturePtBr(TContext context) : base(context)
    {
    }

    protected void Descreva(string description, Feature<TContext> feature) =>
        Describe(description, feature);
}