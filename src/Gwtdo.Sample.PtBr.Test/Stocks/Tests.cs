using Gwtdo.PtBr;
using Gwtdo.Sample.Stocks;
using Xunit;

namespace Gwtdo.Sample.PtBr.Test.Stocks
{
    public class TransacoesNaBolsaPeloUsuario: Feature<StockFixture>
    {
        public TransacoesNaBolsaPeloUsuario() => Fixture = new StockFixture(new Stock());

        [Fact]
        public void Usuario_requisitando_venda_de_acao()
        {
            Dado.Eu_tenho_100_acoes_MSFT();
            Quando.Eu_solicito_a_venda_de_20_acoes_MSFT();
            Entao.Eu_devo_ter_80_acoes_MSFT();
        }        

        [Fact]
        public void Usuario_requisitando_venda_de_acao_antes_do_horario_limite()
        {
            DADO
                .Eu_tenho_100_acoes_MSFT()
                .E.Eu_tenho_150_acoes_APPL()
                .E.Antes_de_fechar_o_pregao();
            QUANDO
                .Eu_solicito_a_venda_de_20_acoes_MSFT();
            ENTAO
                .Eu_devo_ter_80_acoes_MSFT()
                .E.Eu_devo_ter_150_acoes_APPL()
                .E.Uma_oredem_de_venda_de_20_acoes_MSFT_deve_ter_sido_executada();
        }        
    }
}