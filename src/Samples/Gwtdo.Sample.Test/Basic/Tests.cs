using Xunit;

namespace Gwtdo.Sample.Test.Basic;

public class Tests : Feature<Context>, IClassFixture<Context>
{
    public Tests(Context context): base(context)
    {
        context.Setup();
    }

    [Fact]
    public void user_requests_a_sell()
    {
        GIVEN
            .I_have_100_shares_of_MSFT_stock();
        WHEN
            .I_ask_to_sell_20_shares_of_MSFT_stock();
        THEN
            .I_should_have_80_shares_of_MSFT_stock();
    }

    [Fact]
    public void user_requests_a_sell_before_close_of_trading()
    {
        GIVEN
            .I_have_100_shares_of_MSFT_stock()
            .And.I_have_150_shares_of_APPL_stock()
            .And.The_time_is_before_close_of_trading();
        WHEN
            .I_ask_to_sell_20_shares_of_MSFT_stock();
        THEN
            .I_should_have_80_shares_of_MSFT_stock()
            .And.I_should_have_150_shares_of_APPL_stock()
            .And.A_sell_order_for_20_shares_of_MSFT_stock_should_have_been_executed();
    }
}