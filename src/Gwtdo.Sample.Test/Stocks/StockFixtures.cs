using System;
using FluentAssertions;
using Gwtdo.Sample.Stocks;

namespace Gwtdo.Sample.Test.Stocks
{
    public record StockFixture (Stock Stocks) : IFixture;

    public static class Setup
    {
        public static Arrange<StockFixture> I_have_100_shares_of_MSFT_stock(this Arrange<StockFixture> fixtures)
        {
            fixtures.Value.Stocks.Buy("MSFT", 100);
            return fixtures;
        }

        public static Arrange<StockFixture> I_have_150_shares_of_APPL_stock(this Arrange<StockFixture> fixtures)
        {
            fixtures.Value.Stocks.Buy("APPL", 150);
            return fixtures;
        }

        public static Arrange<StockFixture> The_time_is_before_close_of_trading(this Arrange<StockFixture> fixtures)
        {
            var date = DateTime.Today.ToString("yyyy-MM-dd");
            const string time = "23:59:59";

            fixtures.Value.Stocks.SetTimeToCloseTrading($"{date} {time}");
            return fixtures;
        }
    }

    public static class Exercise
    {
        public static Act<StockFixture> I_ask_to_sell_20_shares_of_MSFT_stock(this Act<StockFixture> fixtures)
        {
            fixtures.Value.Stocks.Sell("MSFT", 20);
            return fixtures;
        }
    }

    public static class Verify
    {
        public static Assert<StockFixture> I_should_have_80_shares_of_MSFT_stock(this Assert<StockFixture> fixtures)
        {
            fixtures.Value.Stocks.Shares["MSFT"].Should().Be(80);
            return fixtures;
        }

        public static Assert<StockFixture> I_should_have_150_shares_of_APPL_stock(this Assert<StockFixture> fixtures)
        {
            fixtures.Value.Stocks.Shares["APPL"].Should().Be(150);
            return fixtures;
        }

        public static Assert<StockFixture> A_sell_order_for_20_shares_of_MSFT_stock_should_have_been_executed(
            this Assert<StockFixture> fixtures)
        {
            fixtures.Value.Stocks.Orders["MSFT"].Should().Be(20);
            return fixtures;
        }
    }
}