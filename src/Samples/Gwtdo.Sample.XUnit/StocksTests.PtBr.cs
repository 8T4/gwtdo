using Gwtdo.Attributes;
using Xunit.Abstractions;
using Gwtdo.Sample.XUnit;
using Gwtdo.Sample.XUnit.Localizations;

namespace Gwtdo.Sample.XUnit;

public class StocksTestsPtBr: FeaturePtBr<TradingContext, TradingFixture>, IClassFixture<TradingContext>
{
    public StocksTestsPtBr(ITestOutputHelper output, TradingContext context) : base(context)
    {
        SetOutputRedirect(new TestOutputRedirect(output));
    }
    
    [Theory]
    [InlineData(100, 20, 80, "MSFT")]
    [InlineData(100, 50, 50, "APPL")]
    [Scenario(@"Usuário solicita a venda de ações")]
    public void test_theory_with_attribute_mapping(int share, int sells, int total, string asset)
    {
        Let.Load(new { x = share, y = asset, z = sells, w = total });

        Descreva("User trades stocks",
            DADO
            | @"Eu tenho :x ações da :y" |
            QUANDO
            | @"Peço para vender :z ações da :y" |
            ENTAO
            | @"Eu deveria ter :w ações de :y");
    }    
}