using FluentAssertions;
using Gwtdo.Localizations.PtBr;
using Gwtdo.Sample.Test.NaturalLanguage;
using Xunit;
using Xunit.Abstractions;

namespace Gwtdo.Sample.Test.Mapping.PtBr;

public class TestsPtBr : FeaturePtBr<Context, FixturePtBr>, IClassFixture<Context>
{
    public TestsPtBr(Context context, ITestOutputHelper output) : base(context)
    {
        CENARIO.RedirectStandardOutput = output.WriteLine;
        context.Setup();
    }

    [Fact]
    public void quickly_sample()
    {
        
        CENARIO[@"Trade do usuário"] =
            DESCREVA 
            | @"Usuário solicita venda antes do fechamento do pregão" |
            DADO 
            | @"Eu tenho 100 de açoões MSFT" |
            QUANDO 
            | @"Eu solicito a venda de 20 ações de MSFT" |
            ENTAO 
            | @"Eu devo ter 80 ações de MSFT";

        CENARIO.Execute().IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData(100, 20, 80, "MSFT")]
    [InlineData(100, 50, 50, "APPL")]
    [InlineData(100, 30, 70, "XYZW")]
    public void sample_using_let(int share, int sells, int total, string stock)
    {
        Let["share"] = share;
        Let["sells"] = sells;
        Let["total"] = total;
        Let["stock"] = stock;

        CENARIO[@"Trade do usuário"] =
            DESCREVA
            | @"Usuário solicita venda antes do fechamento do pregão" |
            DADO
            | @"Eu tenho :share ações de :stock" |
            QUANDO
            | @"Eu solicito a venda de :sells ações de :stock" |
            ENTAO
            | @"Eu devo ter :total ações de :stock";

        CENARIO.Execute().IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void more_complex_sample()
    {
        CENARIO[@"Trade do usuário before close of trading"] =
            DESCREVA
            | @"Usuário solicita venda antes do fechamento do pregão" |
            DADO
            | @"Eu tenho 100 de açoões MSFT" | E
            | @"Eu tenho 150 ações de APPL" | E
            | @"Antes do fechamento do pregão" |
            QUANDO
            | @"Eu solicito a venda de 20 ações de MSFT" |
            ENTAO
            | @"Eu devo ter 150 ações de APPL" | E
            | @"Eu devo ter 80 ações de MSFT";

        CENARIO.Execute().IsSuccess.Should().BeTrue();
    }
}