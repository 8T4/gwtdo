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
    public void test_using_scenario_fixture_a()
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
    [InlineData(100, 20, 80, "MSFT")]
    [InlineData(100, 50, 50, "APPL")]
    [InlineData(100, 30, 70, "XYZW")]
    public void test_using_scenario_fixture_b(int share, int sells, int total, string asset)
    {
        Let["share"] = share;
        Let["sells"] = sells;
        Let["total"] = total;
        Let["asset"] = asset;

        SCENARIO["User trades stocks"] =
            DESCRIBE
            | "User requests a sell before close of trading" |
            GIVEN
            | "I have :share shares of :asset stock" |
            WHEN
            | "I ask to sell :sells shares of :asset stock" |
            THEN
            | "I should have :total shares of :asset stock";

        SCENARIO.Execute().IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void test_using_scenario_fixture_c()
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