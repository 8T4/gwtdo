using System;
using FluentAssertions;
using Gwtdo.Sample.Stocks;

namespace Gwtdo.Sample.Test.Stocks
{
    using arrange = Arrange<StockFixture>;
    using act = Act<StockFixture>;
    using assert = Assert<StockFixture>;
    
    public record StockFixture (Stock Stocks) : IFixture;

    public static class Setup
    {
        public static arrange I_have_100_shares_of_MSFT_stock(this arrange fixtures)
        {
            fixtures.Value.Stocks.Buy("MSFT", 100);
            return fixtures;
        }

        public static arrange I_have_150_shares_of_APPL_stock(this arrange fixtures)
        {
            fixtures.Value.Stocks.Buy("APPL", 150);
            return fixtures;
        }

        public static arrange The_time_is_before_close_of_trading(this arrange fixtures)
        {
            var date = DateTime.Today.ToString("yyyy-MM-dd");
            const string time = "23:59:59";

            fixtures.Value.Stocks.SetTimeToCloseTrading($"{date} {time}");
            return fixtures;
        }
    }

    public static class Exercise
    {
        public static act I_ask_to_sell_20_shares_of_MSFT_stock(this act fixtures)
        {
            fixtures.Value.Stocks.Sell("MSFT", 20);
            return fixtures;
        }
    }
    
    public static class Verify
    {
        public static assert I_should_have_80_shares_of_MSFT_stock(this assert fixtures) =>
            fixtures.Verify(x => x.Stocks.Shares["MSFT"].Should().Be(80));

        public static assert I_should_have_150_shares_of_APPL_stock(this assert fixtures) =>
            fixtures.Verify(x => x.Stocks.Shares["APPL"].Should().Be(150));

        public static assert A_sell_order_for_20_shares_of_MSFT_stock_should_have_been_executed(this assert fixtures) => 
            fixtures.Verify(x => x.Stocks.Orders["MSFT"].Should().Be(20));
    }
}