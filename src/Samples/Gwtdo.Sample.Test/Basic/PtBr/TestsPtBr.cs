using Gwtdo.Localizations;
using Gwtdo.Localizations.PtBr;
using Xunit;

namespace Gwtdo.Sample.Test.Basic.PtBr;

public class TestsPtBr : FeaturePtBr<Context>, IClassFixture<Context>
{
    public TestsPtBr(Context context): base(context)
    {
        context.Setup();
    }

    [Fact]
    public void user_requests_a_sell()
    {
        DADO
            .Eu_tenho_100_acoes_de_MSFT();
        QUANDO
            .Eu_solicito_a_venda_de_20_acoes_de_MSFT();
        ENTAO
            .Eu_devo_ter_80_acoes_de_MSFT();
    }

    [Fact]
    public void user_requests_a_sell_before_close_of_trading()
    {
        DADO
            .Eu_tenho_100_acoes_de_MSFT().And
            .Eu_tenho_150_acoes_de_APPL().And
            .Esta_proximo_ao_fechamento_do_pregao();
        QUANDO
            .Eu_solicito_a_venda_de_20_acoes_de_MSFT();
        ENTAO
            .Eu_devo_ter_80_acoes_de_MSFT().And
            .Eu_devo_ter_150_acoes_de_APPL().And
            .A_ordem_de_venda_de_20_acoes_de_MSFT_deve_ter_sido_executada();
    }
}