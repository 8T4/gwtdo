using System;
using FluentAssertions;
using Gwtdo.PtBr;
using Gwtdo.Sample.Stocks;

namespace Gwtdo.Sample.PtBr.Test.Stocks
{
    using arrange = Configuracao<StockFixture>;
    using act = Chamada<StockFixture>;
    using assert = Afirmacao<StockFixture>;
    
    public record StockFixture (Stock Stocks) : IFixture;
    
    public static class Setup
    {
        public static arrange Eu_tenho_100_acoes_MSFT(this arrange fixtures) =>
            fixtures.Iniciar((f) => f.Stocks.Buy("MSFT", 100));

        public static arrange Eu_tenho_150_acoes_APPL(this arrange fixtures) =>
            fixtures.Iniciar((f) => f.Stocks.Buy("APPL", 150));

        public static arrange Antes_de_fechar_o_pregao(this arrange fixtures)
        {
            var date = DateTime.Today.ToString("yyyy-MM-dd");
            const string time = "23:59:59";

            fixtures.Value.Stocks.SetTimeToCloseTrading($"{date} {time}");
            return fixtures;
        }
    }
    
    public static class Exercise
    {
        public static act Eu_solicito_a_venda_de_20_acoes_MSFT(this act fixtures) =>
            fixtures.Excecute(f => f.Stocks.Sell("MSFT", 20));
    }    
    
    public static class Verify
    {
        public static assert Eu_devo_ter_80_acoes_MSFT(this assert fixtures) =>
            fixtures.Validar(x => x.Stocks.Shares["MSFT"].Should().Be(80));

        public static assert Eu_devo_ter_150_acoes_APPL(this assert fixtures) =>
            fixtures.Validar(x => x.Stocks.Shares["APPL"].Should().Be(150));

        public static assert Uma_oredem_de_venda_de_20_acoes_MSFT_deve_ter_sido_executada(this assert fixtures) =>
            fixtures.Validar(x => x.Stocks.Orders["MSFT"].Should().Be(20));
    }    
}