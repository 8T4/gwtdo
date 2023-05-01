using FluentAssertions;
using Gwtdo.Steps;

namespace Gwtdo.Sample;

using ARRANGE = Arrange<TradingContext>;
using ACT = Act<TradingContext>;
using ASSERT = Assert<TradingContext>;

/// <summary>
/// Gwtdo extension methods mapping strategy
/// </summary>
public static class TradingMethods
{
    public static ARRANGE I_have_100_shares_of_MSFT_stock(this ARRANGE fixtures) =>
        fixtures.Setup(f => f.Trading.Buy(new TradingOrder("MSFT", 100, new DateTime(2023, 1, 1, 10, 0, 0))));

    public static ARRANGE I_have_150_shares_of_APPL_stock(this ARRANGE fixtures) =>
        fixtures.Setup(f => f.Trading.Buy(new TradingOrder("APPL", 150, new DateTime(2023, 1, 1, 10, 0, 0))));

    public static ACT I_ask_to_sell_20_shares_of_MSFT_stock(this ACT fixtures) =>
        fixtures.It(f => f.Trading.Sell(new TradingOrder("MSFT", 20, new DateTime(2023, 1, 1, 10, 0, 0))));

    public static ASSERT I_should_have_80_shares_of_MSFT_stock(this ASSERT fixtures) =>
        fixtures.Expect(x => x.Trading.Shares["MSFT"].Should().Be(80));

    public static ASSERT I_should_have_150_shares_of_APPL_stock(this ASSERT fixtures) =>
        fixtures.Expect(x => x.Trading.Shares["APPL"].Should().Be(150));

    public static ASSERT A_sell_order_for_20_shares_of_MSFT_stock_should_have_been_executed(this ASSERT fixtures) =>
        fixtures.Expect(x => x.Trading.Orders["MSFT"].Should().Be(20));
}