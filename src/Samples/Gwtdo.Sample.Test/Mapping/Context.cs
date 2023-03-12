using Gwtdo.Sample.Stocks;

namespace Gwtdo.Sample.Test.NaturalLanguage;

public class Context : IFeatureContext
{
    public Stock Stocks { get; private set; }
    public void Setup() => Stocks = new Stock();
}