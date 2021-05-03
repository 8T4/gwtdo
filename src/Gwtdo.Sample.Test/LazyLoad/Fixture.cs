using System;
using FluentAssertions;
using Gwtdo.Extensions;
using Gwtdo.Sample.Stocks;
using Gwtdo.Scenarios;

namespace Gwtdo.Sample.Test.LazyLoad
{
    public class StockFixture : IFixture
    {
        public Stock Stocks { get; private set; }

        public void Setup() => Stocks = new Stock();
        public void Clear() => Stocks = null;

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

        public void Setup_user_trades_stocks_scenario()
        {
            Fixture.Clear();
            
            SCENARIO["User trades stocks"] =
                DESCRIBE | "User requests a sell before close of trading" |
                GIVEN | "I have 100 shares of MSFT stock".MapAction(Have100SharesOfMsftStock) |
                WHEN | "I ask to sell 20 shares of MSFT stock".MapAction(AskToSell20SharesOfMsftStock) |
                THEN | "I should have 80 shares of MSFT stock".MapAction(ShouldHave80SharesOfMsftStock);
            
            Fixture.Setup();
        }
        
        public void Setup_user_requests_a_sell_before_close_of_trading()
        {
            Fixture.Clear();
            
            SCENARIO["User trades stocks before close of trading"] =
                DESCRIBE | "User requests a sell before close of trading" |
                GIVEN | "I have 100 shares of MSFT stock".MapAction(Have100SharesOfMsftStock) |
                AND | "I have 150 shares of APPL stock".MapAction(Have150SharesOfApplStock) |
                AND | "The time is before close of trading".MapAction(TheTimeIsBeforeCloseOfTrading) |
                WHEN | "I ask to sell 20 shares of MSFT stock".MapAction(AskToSell20SharesOfMsftStock) |
                THEN | "I should have 150 shares of APPL stock".MapAction(ShouldHave150SharesOfApplStock) |
                AND | "I should have 80 shares of MSFT stock".MapAction(ShouldHave80SharesOfMsftStock);
            
            Fixture.Setup();            
        }        

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