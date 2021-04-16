using System;
using FluentAssertions;
using Gwtdo;
using Gwtdo.Sample.Application;

namespace Gwtdo.Sample.Test
{
    public class Fixture : IFixture
    {
        public Stocks Stocks { get; } = new();
    }

    public static class Setup
    {
        public static Setup<Fixture> I_have_100_shares_of_MSFT_stock(this Setup<Fixture> fixtures)
        {
            fixtures.Value.Stocks.Buy("MSFT", 100);
            return fixtures;
        }

        public static Setup<Fixture> I_have_150_shares_of_APPL_stock(this Setup<Fixture> fixtures)
        {
            fixtures.Value.Stocks.Buy("APPL", 150);
            return fixtures;
        }

        public static Setup<Fixture> The_time_is_before_close_of_trading(this Setup<Fixture> fixtures)
        {
            var date = DateTime.Today.ToString("yyyy-MM-dd");
            const string time = "23:59:59";

            fixtures.Value.Stocks.SetTimeToCloseTrading($"{date} {time}");
            return fixtures;
        }
    }

    public static class Exercise
    {
        public static Exercise<Fixture> I_ask_to_sell_20_shares_of_MSFT_stock(this Exercise<Fixture> fixtures)
        {
            fixtures.Value.Stocks.Sell("MSFT", 20);
            return fixtures;
        }
    }

    public static class Verify
    {
        public static Verify<Fixture> I_should_have_80_shares_of_MSFT_stock(this Verify<Fixture> fixtures)
        {
            fixtures.Value.Stocks.Shares["MSFT"].Should().Be(80);
            return fixtures;
        }

        public static Verify<Fixture> I_should_have_150_shares_of_APPL_stock(this Verify<Fixture> fixtures)
        {
            fixtures.Value.Stocks.Shares["APPL"].Should().Be(150);
            return fixtures;
        }

        public static Verify<Fixture> A_sell_order_for_20_shares_of_MSFT_stock_should_have_been_executed(
            this Verify<Fixture> fixtures)
        {
            fixtures.Value.Stocks.Orders["MSFT"].Should().Be(20);
            return fixtures;
        }
    }
}