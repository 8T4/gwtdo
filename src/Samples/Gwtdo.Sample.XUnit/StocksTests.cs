using Gwtdo.Attributes;
using Xunit.Abstractions;

namespace Gwtdo.Sample.XUnit;

/// <summary>
/// Test based on sample presented by Martin Fowler's blog
/// <see href="https://martinfowler.com/bliki/GivenWhenThen.html"/>
/// </summary>
public class StocksTests : Feature<TradingContext, TradingFixture>, IClassFixture<TradingContext>
{
    public StocksTests(ITestOutputHelper output, TradingContext context) : base(context)
    {
        SetOutputRedirect(new TestOutputRedirect(output));
        //context.Setup();
    }

    [Fact]
    [Scenario("User requests a sell before close of trading")]
    public void test_with_extension_methods()
    {
        FeatureContext.Setup();
        
        GIVEN
            .I_have_100_shares_of_MSFT_stock().And
            .I_have_150_shares_of_APPL_stock();
        WHEN
            .I_ask_to_sell_20_shares_of_MSFT_stock();
        THEN
            .I_should_have_80_shares_of_MSFT_stock().And
            .I_should_have_150_shares_of_APPL_stock().And
            .A_sell_order_for_20_shares_of_MSFT_stock_should_have_been_executed();
    }    
    
    [Fact]
    [Scenario(@"User requests a sell before close of trading")]
    public void test_with_attribute_mapping()
    {
        Describe("#trading",
            GIVEN
            | "I have 100 shares of MSFT stock" | AND
            | "I have 150 shares of APPL stock" | AND
            | "The time is before close of trading" |
            WHEN
            | "I ask to sell 20 shares of MSFT stock" |
            THEN
            | "I should have 80 shares of MSFT stock" | AND
            | "I should have 150 shares of APPL stock" | AND
            | "A sell order for 20 shares of MSFT stock should have been executed");
    }    

    [Theory]
    [InlineData(100, 20, 80, "MSFT")]
    [InlineData(100, 50, 50, "APPL")]
    [Scenario(@"User requests a sell before close of trading")]
    public void test_theory_with_attribute_mapping(int share, int sells, int total, string asset)
    {
        Let.Add(new { share, sells, total, asset });

        Describe("User trades stocks",
            GIVEN
            | "I have :share shares of :asset stock" |
            WHEN
            | "I ask to sell :sells shares of :asset stock" |
            THEN
            | "I should have :total shares of :asset stock");
    }
}