namespace Gwtdo.Sample;

public record TradingContext : IFeatureContext, IFeatureContextLifeCycle
{
    public Trading Trading { get; set; }
    public TradingClock? Clock { get; set; }
    
    public void Setup()
    {
        Clock = new TradingClock(new DateTime(2023, 1, 1, 18, 0, 0));
        Trading = new Trading(Clock);
    }

    public void TearDown()
    {
    }
}