<p align="center">
    <img width="120" src="https://raw.githubusercontent.com/8T4/gwtdo/main/doc/img/logo.png" />
</p>

GWTdo is a .NET library that helps developers write readable tests.
It's a DSL based in Given-When-Then [Given-When-Then](https://martinfowler.com/bliki/GivenWhenThen.html) style which 
could be used in your test projects.

[![.NET](https://github.com/8T4/gwtdo/actions/workflows/dotnet.yml/badge.svg)](https://github.com/8T4/gwtdo/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/8T4/gwtdo/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/8T4/gwtdo/actions/workflows/codeql-analysis.yml)

# Getting Started

## Instalation
This package is available through Nuget Packages: https://www.nuget.org/packages/Gwtdo

| Package |  Version | Downloads | Maintainability |
| ------- | ----- | ----- |----- |
| `GWTdo` | [![NuGet](https://img.shields.io/nuget/v/Gwtdo.svg)](https://www.nuget.org/packages/Gwtdo) | [![Nuget](https://img.shields.io/nuget/dt/Gwtdo.svg)](https://www.nuget.org/packages/Gwtdo) | [![Codacy Badge](https://app.codacy.com/project/badge/Grade/51e1962835f24f65a3813d078061a9ef)](https://www.codacy.com/gh/8T4/gwtdo/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=8T4/gwtdo&amp;utm_campaign=Badge_Grade) |

**Nuget**
```shell
Install-Package Gwtdo
```

**.NET CLI**
```shell
dotnet add package Gwtdo
```

You need these things to run GWTdo:
  - [.NET Standard 2.1](https://docs.microsoft.com/pt-br/dotnet/standard/net-standard)

## Example
For our demonstration we want to test class `Stock.cs`([see the code](src/Gwtdo.Sample/Stocks/Stock.cs)) and make sure it covers the following specification:

```yaml
Feature: User trades stocks
  Scenario: User requests a sell before close of trading

    Given I have 100 shares of MSFT stock
    When I ask to sell 20 shares of MSFT stock
    Then I should have 80 shares of MSFT stock
```

### Step 1 - Test fixture 
A test fixture is an environment used to consistently test some item, device, or piece of software. [[1]]()

```c#
public record StockFixture (Stock Stocks) : IFixture;
```
In our example, the `StockFixture` is just a simple `record` ([C# 9.0](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#record-types)) type that contains a
`Stock` instance as a property. All fixtures using `GWTdo` should have implemented `IFixture` interface.

### Step 2 - Extension methods

#### Arrange
Set up the object to be tested. We may need to surround the object with collaborators. For testing purposes, those collaborators might be test objects (mocks, fakes, etc.) or the real thing.
[[2]](https://xp123.com/articles/3a-arrange-act-assert/)

```c#
public static Arrange<StockFixture> I_have_100_shares_of_MSFT_stock(this Arrange<StockFixture> fixtures)
{
    fixtures.Value.Stocks.Buy("MSFT", 100);
    return fixtures;
}
```

#### Act
Act on the object (through some mutator). You may need to give it parameters (again, possibly test objects).
[[2]](https://xp123.com/articles/3a-arrange-act-assert/)

````c#
public static Act<StockFixture> I_ask_to_sell_20_shares_of_MSFT_stock(this Act<StockFixture> fixtures)
{
    fixtures.Value.Stocks.Sell("MSFT", 20);
    return fixtures;
}
````

#### Assert
Make claims about the object, its collaborators, its parameters, and possibly (rarely!!) global state.
[[2]](https://xp123.com/articles/3a-arrange-act-assert/)

````c#
public static Assert<StockFixture> I_should_have_80_shares_of_MSFT_stock(this Assert<StockFixture> fixtures)
{
    fixtures.Value.Stocks.Shares["MSFT"].Should().Be(80);
    return fixtures;
}
````

### Step 3 - Test
Now we are ready to test our code using the `StockFixture` and their extension methods (`Arrange`, `Act` and `Assert`).
For this you should extends `Feature<T>` class and instantiate the fixture in their `Fixture` property. 

#### example (1) - basic

````c#
public class UserTradesStocksFeature : Feature<StockFixture>
{
    public UserTradesStocksFeature()
    {
        Fixture = new StockFixture(new Stock());
    }
    
    [Fact]
    public void Scenario_user_requests_a_sell_before_close_of_trading()
    {
        Given.I_have_100_shares_of_MSFT_stock();
        When.I_ask_to_sell_20_shares_of_MSFT_stock();
        Then.I_should_have_80_shares_of_MSFT_stock();
    }
}    
````

#### example (2) - using dependency injection
_For this, you must have adapted the `StockFixture` class_
````c#
public class UserTradesStocksFeature : Feature<StockFixture>, IClassFixture<StockFixture>
{
    public UserTradesStocksFeature(StockFixture fixture):base(fixture)
    {
    }
    
    [Fact]
    public void Scenario_user_requests_a_sell_before_close_of_trading()
    {
        Given.I_have_100_shares_of_MSFT_stock();
        When.I_ask_to_sell_20_shares_of_MSFT_stock();
        Then.I_should_have_80_shares_of_MSFT_stock();
    }
}    
````

#### example (3) - A little bit complex

````c#
public class UserTradesStocksFeature : Feature<StockFixture>, IClassFixture<StockFixture>
{
    public UserTradesStocksFeature(StockFixture fixture):base(fixture)
    {
    }
    
    [Fact]
    public void Scenario_user_requests_a_sell_before_close_of_trading()
    {
        Given
            .I_have_100_shares_of_MSFT_stock()
            .And.I_have_150_shares_of_APPL_stock()
            .And.The_time_is_before_close_of_trading();

        When
            .I_ask_to_sell_20_shares_of_MSFT_stock();

        Then
            .I_should_have_80_shares_of_MSFT_stock()
            .And.I_should_have_150_shares_of_APPL_stock()
            .And.A_sell_order_for_20_shares_of_MSFT_stock_should_have_been_executed();
    }
}    
````
See those examples in the [sample](src/Gwtdo.Sample.Test/Stocks) project 

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

# References

  - [[1] - Test fixture](https://en.wikipedia.org/wiki/Test_fixture)
  - [[2] - 3A – Arrange, Act, Assert](https://xp123.com/articles/3a-arrange-act-assert/)
  - [[3] - Given-When-Then](https://martinfowler.com/bliki/GivenWhenThen.html)


