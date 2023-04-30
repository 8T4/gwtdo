using System.Diagnostics;
using FluentAssertions;
using Gwtdo.Attributes;
using Gwtdo.Scenarios;

namespace Gwtdo.Sample;

/// <summary>
/// Gwtdo attribute mapping strategy
/// </summary>
public class TradingFixture : ScenarioFixture<TradingContext>
{
    [Given("I have :share shares of :asset stock")]
    [Given(@"Eu tenho :share ações da :asset")]
    public void HaveDynamicSharesOfMsftStock() =>
        Context?.Trading.Buy(new TradingOrder(Let["asset"].As<string>(), Let["share"].As<int>(),
            new DateTime(2023, 1, 1, 10, 0, 0)));

    [Given("I have 100 shares of MSFT stock")]
    public void Have100SharesOfMsftStock() =>
        Context?.Trading.Buy(new TradingOrder("MSFT", 100,
            new DateTime(2023, 1, 1, 10, 0, 0)));

    [Given("The time is before close of trading")]
    public void teste() => Debug.WriteLine("OK");

    [Given("I have 150 shares of APPL stock")]
    public void Have150SharesOfApplStock() =>
        Context?.Trading.Buy(new TradingOrder("APPL", 150,
            new DateTime(2023, 1, 1, 10, 0, 0)));

    [When("I ask to sell :sells shares of :asset stock")]
    [When(@"Peço para vender :sells ações da :asset")]
    public void AskToSellDynamicSharesOfMsftStock() =>
        Context?.Trading.Sell(new TradingOrder(Let["asset"].As<string>(), Let["sells"].As<int>(),
            new DateTime(2023, 1, 1, 10, 0, 0)));

    [When("I ask to sell 20 shares of MSFT stock")]
    public void AskToSell20SharesOfMsftStock() =>
        Context?.Trading.Sell(new TradingOrder("MSFT", 20,
            new DateTime(2023, 1, 1, 10, 0, 0)));

    [Then("I should have :total shares of :asset stock")]
    [Then(@"Eu deveria ter :total ações de :asset")]
    public void ShouldHaveDynamicSharesOfMsftStock() =>
        Context?.Trading.Shares[Let["asset"].As<string>()].Should().Be(Let["total"].As<int>());

    [Then("I should have 80 shares of MSFT stock")]
    public void ShouldHave80SharesOfMsftStock() =>
        Context?.Trading.Shares["MSFT"].Should().Be(80);

    [Then("I should have 150 shares of APPL stock")]
    public void ShouldHave150SharesOfApplStock() =>
        Context?.Trading.Shares["APPL"].Should().Be(150);

    [Then("A sell order for 20 shares of MSFT stock should have been executed")]
    public void Test() =>
        Context?.Trading.Orders["MSFT"].Should().Be(20);
}