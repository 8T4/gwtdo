using System;
using FluentAssertions;
using Gwtdo.Steps;

namespace Gwtdo.Sample.Test.Basic.PtBr;

public static class Fixture
{
    #region DADO - ARRANGE

    public static Arrange<Context> Eu_tenho_100_acoes_de_MSFT(this Arrange<Context> fixtures) =>
        fixtures.Setup(f => f.Stocks.Buy("MSFT", 100));

    public static Arrange<Context> Eu_tenho_150_acoes_de_APPL(this Arrange<Context> fixtures) =>
        fixtures.Setup(f => f.Stocks.Buy("APPL", 150));

    public static void Esta_proximo_ao_fechamento_do_pregao(this Arrange<Context> fixtures)
    {
        var date = DateTime.Today.ToString("yyyy-MM-dd");
        fixtures.Setup(f => f.Stocks.SetTimeToCloseTrading($"{date} 23:59:59"));
    }

    #endregion

    #region QUANDO - ACT

    public static void Eu_solicito_a_venda_de_20_acoes_de_MSFT(this Act<Context> fixtures) =>
        fixtures.It(f => f.Stocks.Sell("MSFT", 20));

    #endregion

    #region ENTAO - ASSERT

    public static Assert<Context> Eu_devo_ter_80_acoes_de_MSFT(this Assert<Context> fixtures) =>
        fixtures.Expect(x => x.Stocks.Shares["MSFT"].Should().Be(80));

    public static Assert<Context> Eu_devo_ter_150_acoes_de_APPL(this Assert<Context> fixtures) =>
        fixtures.Expect(x => x.Stocks.Shares["APPL"].Should().Be(150));

    public static void A_ordem_de_venda_de_20_acoes_de_MSFT_deve_ter_sido_executada(
        this Assert<Context> fixtures) =>
        fixtures.Expect(x => x.Stocks.Orders["MSFT"].Should().Be(20));

    #endregion
}