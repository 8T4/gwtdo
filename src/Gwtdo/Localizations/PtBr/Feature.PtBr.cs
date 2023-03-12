using Gwtdo.Scenarios;
using Gwtdo.Steps;

namespace Gwtdo.Localizations.PtBr;

/// <summary>
/// Although Given-When-Then style is symptomatic to BDD, the basic idea is pretty common when writing tests or
/// specification by example. Meszaros describes the pattern as Four-Phase Test. His four phases are Setup (Given),
/// Exercise (When), Verify (Then) and Teardown [5]. Bill Wake came up with the formulation as Arrange, Act, Assert.
/// <see href="https://martinfowler.com/bliki/GivenWhenThen.html"/>
/// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class FeaturePtBr<T> : Feature<T> where T: IFeatureContext
{
    protected Describe<T> DESCREVA => Describe<T>.Create(this);
    protected Arrange<T> DADO => Arrange<T>.Create(Context);
    protected Act<T> QUANDO => Act<T>.Create(Context);
    protected Assert<T> ENTAO => Assert<T>.Create(Context);
    protected And E => And.Create();
    public Scenario<T> CENARIO => this.SCENARIO;

    protected FeaturePtBr(T context) : base(context)
    {
    }
}