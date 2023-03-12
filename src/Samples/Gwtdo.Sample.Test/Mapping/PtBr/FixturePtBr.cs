using System;
using FluentAssertions;
using Gwtdo.Attributes;
using Gwtdo.Sample.Test.NaturalLanguage;
using Gwtdo.Scenarios;

namespace Gwtdo.Sample.Test.Mapping.PtBr;

public class FixturePtBr : ScenarioFixture<Context>
{
    [Given(@"Eu tenho :share ações de :stock")]
    public void HaveDynamicSharesOfMsftStock() =>
        Context.Stocks.Buy(Let["stock"].As<string>(), Let["share"].As<int>());

    [When(@"Eu solicito a venda de :sells ações de :stock")]
    public void AskToSellDynamicSharesOfMsftStock() =>
        Context.Stocks.Sell(Let["stock"].As<string>(), Let["sells"].As<int>());

    [Then(@"Eu devo ter :total ações de :stock")]
    public void ShouldHaveDynamicSharesOfMsftStock() =>
        Context.Stocks.Shares[Let["stock"].As<string>()].Should().Be(Let["total"].As<int>());

    [Given(@"Eu tenho 100 de açoões MSFT")]
    public void Have100SharesOfMsftStock() =>
        Context.Stocks.Buy(@"MSFT", 100);

    [Given(@"Eu tenho 150 ações de APPL")]
    public void Have150SharesOfApplStock() =>
        Context.Stocks.Buy(@"APPL", 150);
    
    [Given(@"Antes do fechamento do pregão")]
    public void TheTimeIsBeforeCloseOfTrading() =>
        Context.Stocks.SetTimeToCloseTrading($"{DateTime.Today:yyyy-MM-dd} 23:59:59");

    [When(@"Eu solicito a venda de 20 ações de MSFT")]
    public void AskToSell20SharesOfMsftStock() =>
        Context.Stocks.Sell(@"MSFT", 20);

    [Then(@"Eu devo ter 80 ações de MSFT")]
    public void ShouldHave80SharesOfMsftStock() =>
        Context.Stocks.Shares["MSFT"].Should().Be(80);

    [Then(@"Eu devo ter 150 ações de APPL")]
    public void ShouldHave150SharesOfApplStock() =>
        Context.Stocks.Shares["APPL"].Should().Be(150);
}