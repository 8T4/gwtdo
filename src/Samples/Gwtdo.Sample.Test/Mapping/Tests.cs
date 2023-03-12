using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Gwtdo.Sample.Test.NaturalLanguage;

public class Tests : Feature<Context, Fixture>, IClassFixture<Context>
{
    public Tests(Context context, ITestOutputHelper output) : base(context)
    {
        SCENARIO.RedirectStandardOutput = output.WriteLine;
        context.Setup();
    }

    [Fact]
    public void quickly_sample()
    {
        SCENARIO["User trades stocks"] =
            DESCRIBE 
            | "User requests a sell before close of trading" |
            GIVEN 
            | "I have 100 shares of MSFT stock" |
            WHEN 
            | "I ask to sell 20 shares of MSFT stock" |
            THEN 
            | "I should have 80 shares of MSFT stock";

        SCENARIO.Execute().IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData(100, 20, 80)]
    [InlineData(100, 50, 50)]
    [InlineData(100, 30, 70)]
    public void sample_using_let(int share, int sells, int total)
    {
        Let["share"] = share;
        Let["sells"] = sells;
        Let["total"] = total;

        SCENARIO["User trades stocks"] =
            DESCRIBE
            | "User requests a sell before close of trading" |
            GIVEN
            | "I have :share shares of MSFT stock" |
            WHEN
            | "I ask to sell :sells shares of MSFT stock" |
            THEN
            | "I should have :total shares of MSFT stock";

        SCENARIO.Execute().IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void user_requests_a_sell_before_close_of_trading()
    {
        SCENARIO["User trades stocks before close of trading"] =
            DESCRIBE
            | "User requests a sell before close of trading" |
            GIVEN
            | "I have 100 shares of MSFT stock" | AND
            | "I have 150 shares of APPL stock" | AND
            | "The time is before close of trading" |
            WHEN
            | "I ask to sell 20 shares of MSFT stock" |
            THEN
            | "I should have 150 shares of APPL stock" | AND
            | "I should have 80 shares of MSFT stock";

        SCENARIO.Execute().IsSuccess.Should().BeTrue();
    }
}