using FluentAssertions;
using Gwtdo.Extensions;
using Gwtdo.Sample.Stocks;
using Gwtdo.Scenarios;
using System;

namespace Gwtdo.Sample.Test.NaturalLanguange
{
    public class StockFixture : IFixture
    {
        public Stock Stocks { get; private set; }

        public void Setup() => Stocks = new Stock();
        public void Clear() => Stocks = null;
    }

    public class StockFixtureMapper : ScenarioMapper<StockFixture>
    {
        public StockFixtureMapper(Scenario<StockFixture> scenario) : base(scenario)
        {
        }

        public void Setup_user_trades_stocks_scenario_dynamic()
            => SCENARIO["User trades stocks"] =
                DESCRIBE | "User requests a sell before close of trading" |
                GIVEN | "I have :share-value shares of MSFT stock".MapAction(HaveDynamicSharesOfMsftStock) |
                WHEN | "I ask to sell :sells-value shares of MSFT stock".MapAction(AskToSellDynamicSharesOfMsftStock) |
                THEN | "I should have :total-value shares of MSFT stock".MapAction(ShouldHaveDynamicSharesOfMsftStock);

        public void Setup_user_requests_a_sell_before_close_of_trading() => SCENARIO["User trades stocks before close of trading"] =
                DESCRIBE | "User requests a sell before close of trading" |
                GIVEN
                | "I have 100 shares of MSFT stock".MapAction(Have100SharesOfMsftStock) | AND
                | "I have 150 shares of APPL stock".MapAction(Have150SharesOfApplStock) | AND
                | "The time is before close of trading".MapAction(TheTimeIsBeforeCloseOfTrading) |
                WHEN
                | "I ask to sell 20 shares of MSFT stock".MapAction(AskToSell20SharesOfMsftStock) |
                THEN
                | "I should have 150 shares of APPL stock".MapAction(ShouldHave150SharesOfApplStock) | AND
                | "I should have 80 shares of MSFT stock".MapAction(ShouldHave80SharesOfMsftStock);

        private Action<StockFixture> HaveDynamicSharesOfMsftStock =>
            f => f.Stocks.Buy("MSFT", Let.Get<int>("share-value"));

        private Action<StockFixture> AskToSellDynamicSharesOfMsftStock =>
            f => f.Stocks.Sell("MSFT", Let.Get<int>("sells-value"));

        private Action<StockFixture> ShouldHaveDynamicSharesOfMsftStock =>
            f => f.Stocks.Shares["MSFT"].Should().Be(Let.Get<int>("total-value"));

        private static Action<StockFixture> Have100SharesOfMsftStock =>
            f => f.Stocks.Buy("MSFT", 100);

        private static Action<StockFixture> Have150SharesOfApplStock =>
            f => f.Stocks.Buy("APPL", 150);

        private static Action<StockFixture> AskToSell20SharesOfMsftStock =>
            f => f.Stocks.Sell("MSFT", 20);

        private static Action<StockFixture> ShouldHave80SharesOfMsftStock =>
            f => f.Stocks.Shares["MSFT"].Should().Be(80);

        private static Action<StockFixture> ShouldHave150SharesOfApplStock =>
            f => f.Stocks.Shares["APPL"].Should().Be(150);

        private static Action<StockFixture> TheTimeIsBeforeCloseOfTrading =>
            f => f.Stocks.SetTimeToCloseTrading($"{DateTime.Today:yyyy-MM-dd} 23:59:59");
    }
}