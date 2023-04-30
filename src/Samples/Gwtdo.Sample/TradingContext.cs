namespace Gwtdo.Sample;

public record TradingContext : IFeatureContext, IFeatureContextLifeCycle
{
    public Trading Trading { get; set; }
    
    public void Setup() => Trading = new Trading();
    
    public void TearDown()
    {
        //node
    }
}