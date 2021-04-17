using Gwtdo.Sample.Stocks;
using Xunit;

namespace Gwtdo.Sample.Test.Stocks
{
    public class UserTradesStocksFeature : Gwtdo.Feature<StockFixture>
    {
        public UserTradesStocksFeature():base()
        {
            Fixture = new StockFixture(new Stock());
        }

        [Fact]
        public void Scenario_user_requests_a_sell_before_close_of_trading_MSFT_stock()
        {
            Given.I_have_100_shares_of_MSFT_stock();
            When.I_ask_to_sell_20_shares_of_MSFT_stock();
            Then.I_should_have_80_shares_of_MSFT_stock();
        }

        [Fact]
        public void Scenario_user_requests_a_sell_before_close_of_trading()
        {
            Given
                .I_have_100_shares_of_MSFT_stock()
                .And.I_have_150_shares_of_APPL_stock()
                .And.The_time_is_before_close_of_trading();

            When
                .I_ask_to_sell_20_shares_of_MSFT_stock();

            Then
                .I_should_have_80_shares_of_MSFT_stock()
                .And.I_should_have_150_shares_of_APPL_stock()
                .And.A_sell_order_for_20_shares_of_MSFT_stock_should_have_been_executed();
        }
    }
}