using System;
using FluentAssertions;
using Gwtdo.Attributes;
using Gwtdo.Scenarios;

namespace Gwtdo.Sample.Test.NaturalLanguage;

public class Fixture : ScenarioFixture<Context>
{
    [Given("I have :share shares of MSFT stock")]
    public void HaveDynamicSharesOfMsftStock() =>
        Context.Stocks.Buy("MSFT", Let["share"].As<int>());

    [When("I ask to sell :sells shares of MSFT stock")]
    public void AskToSellDynamicSharesOfMsftStock() =>
        Context.Stocks.Sell("MSFT", Let["sells"].As<int>());

    [Then("I should have :total shares of MSFT stock")]
    public void ShouldHaveDynamicSharesOfMsftStock() =>
        Context.Stocks.Shares["MSFT"].Should().Be(Let["total"].As<int>());

    [Given("I have 100 shares of MSFT stock")]
    public void Have100SharesOfMsftStock() =>
        Context.Stocks.Buy("MSFT", 100);

    [Given("I have 150 shares of APPL stock")]
    public void Have150SharesOfApplStock() =>
        Context.Stocks.Buy("APPL", 150);
    
    [Given("The time is before close of trading")]
    public void TheTimeIsBeforeCloseOfTrading() =>
        Context.Stocks.SetTimeToCloseTrading($"{DateTime.Today:yyyy-MM-dd} 23:59:59");

    [When("I ask to sell 20 shares of MSFT stock")]
    public void AskToSell20SharesOfMsftStock() =>
        Context.Stocks.Sell("MSFT", 20);

    [Then("I should have 80 shares of MSFT stock")]
    public void ShouldHave80SharesOfMsftStock() =>
        Context.Stocks.Shares["MSFT"].Should().Be(80);

    [Then("I should have 150 shares of APPL stock")]
    public void ShouldHave150SharesOfApplStock() =>
        Context.Stocks.Shares["APPL"].Should().Be(150);
}