**GWTDO is a .NET library that helps developers write readable tests**.
It's a DSL based on the [Given-When-Then](https://martinfowler.com/bliki/GivenWhenThen.html) style which could be used in your test projects.

[![.NET](https://github.com/8T4/gwtdo/actions/workflows/dotnet.yml/badge.svg)](https://github.com/8T4/gwtdo/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/8T4/gwtdo/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/8T4/gwtdo/actions/workflows/codeql-analysis.yml)

<p align="center" width="100%">
    <img src="https://raw.githubusercontent.com/8T4/gwtdo/main/doc/img/banner.png" />
</p

# Getting Started

## Instalation
This package is available through Nuget Packages (https://www.nuget.org/packages/Gwtdo).

| Package |  Version | Downloads | Maintainability |
| ------- | ----- | ----- |----- |
| `GWTDO` | [![NuGet](https://img.shields.io/nuget/v/Gwtdo.svg)](https://www.nuget.org/packages/Gwtdo) | [![Nuget](https://img.shields.io/nuget/dt/Gwtdo.svg)](https://www.nuget.org/packages/Gwtdo) | [![Codacy Badge](https://app.codacy.com/project/badge/Grade/51e1962835f24f65a3813d078061a9ef)](https://www.codacy.com/gh/8T4/gwtdo/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=8T4/gwtdo&amp;utm_campaign=Badge_Grade) |

## Approaches

#### Natural Language
use the `GWTDO` DSL to facilitate natural language writing. With it, you write more readable tests with low formal language interference.

```c#
public void user_requests_a_sell()
{
    SCENARIO["User trades stocks"] =
        DESCRIBE | "User requests a sell before close of trading" |
           GIVEN | "I have 100 shares of MSFT stock" |
            WHEN | "I ask to sell 20 shares of MSFT stock" |
            THEN | "I should have 80 shares of MSFT stock";
   ...
}
```

Then map your specification as follows

```c#
public void Setup_user_trades_stocks_scenario()
{
    SCENARIO["User trades stocks"] =
        DESCRIBE | "User requests a sell before close of trading" |
           GIVEN | "I have 100 shares of MSFT stock".MapAction(Have100SharesOfMsftStock) |
            WHEN | "I ask to sell 20 shares of MSFT stock".MapAction(AskToSell20SharesOfMsftStock) |
            THEN | "I should have 80 shares of MSFT stock".MapAction(ShouldHave80SharesOfMsftStock);
}

private static Action<StockFixture> Have100SharesOfMsftStock => f => f.Stocks.Buy("MSFT", 100);

private static Action<StockFixture> Have150SharesOfApplStock => f => f.Stocks.Buy("APPL", 150);        

private static Action<StockFixture> AskToSell20SharesOfMsftStock => f => f.Stocks.Sell("MSFT", 20);

```
See the complete code in the [Natural Languange sample code](https://github.com/8T4/gwtdo/tree/main/src/samples/Gwtdo.Sample.Test/NaturalLanguange).


#### Just code

If you prefer, you can only use code to write your specifications

```c#

using arrange = Arrange<StockFixture>;
using act = Act<StockFixture>;
using assert = Assert<StockFixture>;

[Fact]
public void user_requests_a_sell()
{
    GIVEN.I_have_100_shares_of_MSFT_stock();
    WHEN.I_ask_to_sell_20_shares_of_MSFT_stock();
    THEN.I_should_have_80_shares_of_MSFT_stock();
}

public static arrange I_have_100_shares_of_MSFT_stock(this arrange fixtures) =>
    fixtures.Setup((f) => f.Stocks.Buy("MSFT", 100));
    
public static act I_ask_to_sell_20_shares_of_MSFT_stock(this act fixtures) =>
    fixtures.Excecute(f => f.Stocks.Sell("MSFT", 20));
    
public static assert I_should_have_80_shares_of_MSFT_stock(this assert fixtures) =>
    fixtures.Verify(x => x.Stocks.Shares["MSFT"].Should().Be(80));    
```
        
See the complete code in the [Just Code sample](https://github.com/8T4/gwtdo/tree/main/src/samples/Gwtdo.Sample.Test/JustCode).


## Guide to contributing to a GitHub project
This is a guide to contributing to this open source project that uses GitHub. It’s mostly based on how many open sorce projects operate. That’s all there is to it. The fundamentals are:

- Fork the project & clone locally.  
- Create an upstream remote and sync your local copy before you branch.  
- Branch for each separate piece of work.  
- Do the work, write good commit messages, and read the CONTRIBUTING file if there is one.  
- Push to your origin repository.  
- Create a new PR in GitHub.  
- Respond to any code review feedback.  

If you want to contribute to an open source project, the best one to pick is one that you are using yourself. The maintainers will appreciate it!

## References

- [[1] - Given-When-Then](https://martinfowler.com/bliki/GivenWhenThen.html)
- [[2] - Test fixture](https://en.wikipedia.org/wiki/Test_fixture)  
- [[3] - 3A – Arrange, Act, Assert](https://xp123.com/articles/3a-arrange-act-assert/)  
