using Xunit;
using Gwtdo;

namespace Gwtdo.Sample.Test
{
    
    public class UserTradesStocksFeature: Feature<Fixture>, IClassFixture<Fixture>
    {
        public UserTradesStocksFeature(Fixture fixture) : base(fixture)
        {
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