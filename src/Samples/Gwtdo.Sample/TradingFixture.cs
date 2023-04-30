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
    [Given("I have :x shares of :y stock")]
    [Given(@"Eu tenho :x ações da :y")]
    public void HaveDynamicSharesOfMsftStock() =>
        Context?.Trading.Buy(new TradingOrder(Let["y"].As<string>(), Let["x"].As<int>(),
            new DateTime(2023, 1, 1, 10, 0, 0)));

    [Given("I have 100 shares of MSFT stock")]
    public void Have100SharesOfMsftStock() =>
        Context?.Trading.Buy(new TradingOrder("MSFT", 100,
            new DateTime(2023, 1, 1, 10, 0, 0)));

    [Given("The time is before close of trading")]
    public void teste() => Context.Clock.UpdateLimit(new DateTime(2023, 1, 1, 18, 0, 0));

    [Given("I have 150 shares of APPL stock")]
    public void Have150SharesOfApplStock() =>
        Context?.Trading.Buy(new TradingOrder("APPL", 150,
            new DateTime(2023, 1, 1, 10, 0, 0)));

    [When("I ask to sell :z shares of :y stock")]
    [When(@"Peço para vender :z ações da :y")]
    public void AskToSellDynamicSharesOfMsftStock() =>
        Context?.Trading.Sell(new TradingOrder(Let["y"].As<string>(), Let["z"].As<int>(),
            new DateTime(2023, 1, 1, 10, 0, 0)));

    [When("I ask to sell 20 shares of MSFT stock")]
    public void AskToSell20SharesOfMsftStock() =>
        Context?.Trading.Sell(new TradingOrder("MSFT", 20,
            new DateTime(2023, 1, 1, 10, 0, 0)));

    [When("I ask to buy 20 shares of MSFT stock")]
    public void AskToBuy20SharesOfMsftStock() =>
        Context?.Trading.Buy(new TradingOrder("MSFT", 20,
            new DateTime(2023, 1, 1, 10, 0, 0)));

    [Then("I should have :w shares of :y stock")]
    [Then(@"Eu deveria ter :w ações de :y")]
    public void ShouldHaveDynamicSharesOfMsftStock() =>
        Context?.Trading.Shares[Let["y"].As<string>()].Should().Be(Let["w"].As<int>());

    [Then("I should have 80 shares of MSFT stock")]
    public void ShouldHave80SharesOfMsftStock() =>
        Context?.Trading.Shares["MSFT"].Should().Be(80);
    
    [Then("I should have 120 shares of MSFT stock")]
    public void ShouldHave120SharesOfMsftStock() =>
        Context?.Trading.Shares["MSFT"].Should().Be(120);    

    [Then("I should have 150 shares of APPL stock")]
    public void ShouldHave150SharesOfApplStock() =>
        Context?.Trading.Shares["APPL"].Should().Be(150);

    [Then("A sell order for 20 shares of MSFT stock should have been executed")]
    public void ASellOrderFor20SharesOfMSFT() =>
        Context?.Trading.Orders["MSFT"].Should().Be(20);
    
    [Then("A sell order for 0 shares of MSFT stock should have been executed")]
    public void ASellOrderFor0SharesOfMSFT() =>
        Context?.Trading.Orders.ContainsKey("MSFT").Should().BeFalse();    
}