using System;
using FluentAssertions;
using Gwtdo.Extensions;
using Gwtdo.Sample.Stocks;
using Gwtdo.Scenarios;

namespace Gwtdo.Sample.Test.LazyLoad
{
    public class StockFixture : IFixture
    {
        public Stock Stocks { get; }

        public StockFixture()
        {
            Stocks = new Stock();
        }
    }

    public class StockFixtureMapper : ScenarioMapper<StockFixture>
    {
        public StockFixtureMapper(Scenario<StockFixture> scenario, StockFixture fixture) : base(scenario, fixture)
        {
        }

        public void Setup_User_Trades_Stocks_Scenario()
        {
            SCENARIO["User trades stocks"] =
                DESCRIBE | "User requests a sell before close of trading" |
                GIVEN | "I have 100 shares of MSFT stock".MapAction(I_have_100_shares_of_MSFT_stock()) |
                WHEN | "I ask to sell 20 shares of MSFT stock".MapAction(I_ask_to_sell_20_shares_of_MSFT_stock()) |
                THEN | "I should have 80 shares of MSFT stock".MapAction(I_should_have_80_shares_of_MSFT_stock());
        }

        private static Action<StockFixture> I_have_100_shares_of_MSFT_stock() =>
            f => f.Stocks.Buy("MSFT", 100);

        private static Action<StockFixture> I_ask_to_sell_20_shares_of_MSFT_stock() =>
            f => f.Stocks.Sell("MSFT", 20);

        private static Action<StockFixture> I_should_have_80_shares_of_MSFT_stock() =>
            f => f.Stocks.Shares["MSFT"].Should().Be(80);
    }
}