namespace Gwtdo.Steps;

public abstract class Step<T>  where T : class
{
    protected T Value { get; }
    public Feature<T> Feature { get; }

    protected Step(Feature<T> feature)
    {
        Feature = feature;
        Value = feature.Scenario.Context;
    }
}