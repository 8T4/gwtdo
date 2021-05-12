using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Gwtdo.Sample.Test.NaturalLanguange
{
    public class UserTradesStocks : Feature<StockFixture>, IClassFixture<StockFixture>
    {
        private StockFixtureMapper Mapper { get; }

        public UserTradesStocks(StockFixture fixture, ITestOutputHelper output) : base(fixture)
        {
            SCENARIO.RedirectStandardOutput = output.WriteLine;
            Mapper = new StockFixtureMapper(SCENARIO);
            fixture.Setup();
        }

        [Fact]
        public void user_requests_a_sell()
        {
            SCENARIO["User trades stocks"] =
                DESCRIBE | "User requests a sell before close of trading" |
                GIVEN
                | "I have 100 shares of MSFT stock" |
                WHEN
                | "I ask to sell 20 shares of MSFT stock" |
                THEN
                | "I should have 80 shares of MSFT stock";

            Mapper.Setup_user_trades_stocks_scenario();
            var result = SCENARIO.Execute();
            result.IsSuccess.Should().BeTrue(result.Message);
        }

        [Theory]
        [InlineData(100, 20, 80)]
        [InlineData(100, 50, 50)]
        [InlineData(100, 30, 70)]
        public void user_requests_a_sell_dynamic(int share, int sells, int total)
        {
            Let["share-value"] = share;
            Let["sells-value"] = sells;
            Let["total-value"] = total;

            SCENARIO["User trades stocks"] =
                DESCRIBE | "User requests a sell before close of trading" |
                GIVEN
                | "I have :share-value shares of MSFT stock" |
                WHEN
                | "I ask to sell :sells-value shares of MSFT stock" |
                THEN
                | "I should have :total-value shares of MSFT stock";

            Mapper.Setup_user_trades_stocks_scenario_dynamic();
            var result = SCENARIO.Execute();
            result.IsSuccess.Should().BeTrue(result.Message);
        }

        [Fact]
        public void user_requests_a_sell_before_close_of_trading()
        {
            SCENARIO["User trades stocks before close of trading"] =
                DESCRIBE | "User requests a sell before close of trading" |
                GIVEN
                | "I have 100 shares of MSFT stock" | AND
                | "I have 150 shares of APPL stock" | AND
                | "The time is before close of trading" |
                WHEN
                | "I ask to sell 20 shares of MSFT stock" |
                THEN
                | "I should have 150 shares of APPL stock" | AND
                | "I should have 80 shares of MSFT stock";

            Mapper.Setup_user_requests_a_sell_before_close_of_trading();
            var result = SCENARIO.Execute();
            result.IsSuccess.Should().BeTrue(result.Message);
        }
    }
}