using System;
using FluentAssertions;
using Gwtdo.Steps;

namespace Gwtdo.Sample.Test.Basic;

public static class Fixture
{
    #region GIVEN - ARRANGE

    public static Arrange<Context> I_have_100_shares_of_MSFT_stock(this Arrange<Context> fixtures) =>
        fixtures.Setup(f => f.Stocks.Buy("MSFT", 100));

    public static Arrange<Context> I_have_150_shares_of_APPL_stock(this Arrange<Context> fixtures) =>
        fixtures.Setup(f => f.Stocks.Buy("APPL", 150));

    public static void The_time_is_before_close_of_trading(this Arrange<Context> fixtures)
    {
        var date = DateTime.Today.ToString("yyyy-MM-dd");
        fixtures.Setup(f => f.Stocks.SetTimeToCloseTrading($"{date} 23:59:59"));
    }

    #endregion

    #region WHEN - ACT

    public static void I_ask_to_sell_20_shares_of_MSFT_stock(this Act<Context> fixtures) =>
        fixtures.It(f => f.Stocks.Sell("MSFT", 20));

    #endregion

    #region THEN - ASSERT

    public static Assert<Context> I_should_have_80_shares_of_MSFT_stock(this Assert<Context> fixtures) =>
        fixtures.Expect(x => x.Stocks.Shares["MSFT"].Should().Be(80));

    public static Assert<Context> I_should_have_150_shares_of_APPL_stock(this Assert<Context> fixtures) =>
        fixtures.Expect(x => x.Stocks.Shares["APPL"].Should().Be(150));

    public static void A_sell_order_for_20_shares_of_MSFT_stock_should_have_been_executed(
        this Assert<Context> fixtures) =>
        fixtures.Expect(x => x.Stocks.Orders["MSFT"].Should().Be(20));

    #endregion
}