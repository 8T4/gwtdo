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
        Let.Add(new { share, sells, total, asset });

        Descreva("User trades stocks",
            DADO
            | @"Eu tenho :share ações da :asset" |
            QUANDO
            | @"Peço para vender :sells ações da :asset" |
            ENTAO
            | @"Eu deveria ter :total ações de :asset");
    }    
}