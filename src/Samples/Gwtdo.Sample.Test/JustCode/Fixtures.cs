using System;
using FluentAssertions;
using Gwtdo.Sample.Stocks;

namespace Gwtdo.Sample.Test.JustCode
{
    using arrange = Arrange<StockFixture>;
    using act = Act<StockFixture>;
    using assert = Assert<StockFixture>;

    public class StockFixture: IFixture
    {
        public Stock Stocks { get; private set; }
        public void Setup() =>  Stocks = new Stock();
    } 

    public static class Setup
    {
        public static arrange I_have_100_shares_of_MSFT_stock(this arrange fixtures) =>
            fixtures.Setup((f) => f.Stocks.Buy("MSFT", 100));

        public static arrange I_have_100_shares_of_MSFT_stock_async(this arrange fixtures) =>
            fixtures.Setup(async (f) => await f.Stocks.BuyAsync("MSFT", 100));

        public static arrange I_have_150_shares_of_APPL_stock(this arrange fixtures) =>
            fixtures.Setup((f) => f.Stocks.Buy("APPL", 150));

        public static arrange The_time_is_before_close_of_trading(this arrange fixtures)
        {
            var date = DateTime.Today.ToString("yyyy-MM-dd");
            return fixtures.Setup((f) => f.Stocks.SetTimeToCloseTrading($"{date} 23:59:59"));
        }
    }

    public static class Exercise
    {
        public static act I_ask_to_sell_20_shares_of_MSFT_stock(this act fixtures) =>
            fixtures.It(f => f.Stocks.Sell("MSFT", 20));
    }

    public static class Verify
    {
        public static assert I_should_have_80_shares_of_MSFT_stock(this assert fixtures) =>
            fixtures.Expect(x => x.Stocks.Shares["MSFT"].Should().Be(80));

        public static assert I_should_have_150_shares_of_APPL_stock(this assert fixtures) =>
            fixtures.Expect(x => x.Stocks.Shares["APPL"].Should().Be(150));

        public static assert A_sell_order_for_20_shares_of_MSFT_stock_should_have_been_executed(this assert fixtures) =>
            fixtures.Expect(x => x.Stocks.Orders["MSFT"].Should().Be(20));
    }
}