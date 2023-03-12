<p align='center'>
    <img src="https://raw.githubusercontent.com/8T4/gwtdo/main/doc/img/icon.png">
    <br/>It´s a dotnet library that helps developers write readable tests.
    <br/>Also, it's a DSL based on the <strong>Given-When-Then</strong> style which could be used in your test projects.
    <br/>
    <br/>
    <a href='https://github.com/8T4/gwtdo/actions/workflows/dotnet.yml'><img src="https://github.com/8T4/gwtdo/actions/workflows/dotnet.yml/badge.svg"></a>
    <a href='https://github.com/8T4/gwtdo/actions/workflows/codeql-analysis.yml'><img src="https://github.com/8T4/gwtdo/actions/workflows/codeql-analysis.yml/badge.svg"></a>
    <a href='https://www.nuget.org/packages/Gwtdo'><img src="https://img.shields.io/nuget/v/Gwtdo.svg"></a>
    <a href='https://www.nuget.org/packages/Gwtdo'><img src="https://img.shields.io/nuget/dt/Gwtdo.svg"></a>
    <a href='https://www.codacy.com/gh/8T4/gwtdo/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=8T4/gwtdo&amp;utm_campaign=Badge_Grade'><img src="https://app.codacy.com/project/badge/Grade/51e1962835f24f65a3813d078061a9ef"></a>
</p>


# Getting Started 

## Use base classes
Use the class `Feature<TContext, TFixture>` to adopt Given-When-Then approach in your tests. 

```c#
public class Tests : Feature<Context, Fixture>, IClassFixture<Context>
{
    public Tests(Context context, ITestOutputHelper output) : base(context)
    {
        //Configuring to use the ITestOutputHelper
        SCENARIO.RedirectStandardOutput = output.WriteLine;
        context.Setup();
    }
    
    ...
}

//TContext
public class Context : IFeatureContext
{
    public Stock Stocks { get; private set; }
    public void Setup() => Stocks = new Stock();
}
```

## Write a scenario
Specify your test using natural language within a C # method. Easy, simple and fast.

#### english (en-us)
```c#
[Fact]
public void quickly_sample()
{
    SCENARIO["User trades stocks"] =
        DESCRIBE 
        | "User requests a sell before close of trading" |
        GIVEN 
        | "I have 100 shares of MSFT stock" |
        WHEN 
        | "I ask to sell 20 shares of MSFT stock" |
        THEN 
        | "I should have 80 shares of MSFT stock";
        
    SCENARIO.Execute().IsSuccess.Should().BeTrue();
}
```
See the complete code [here](src/Samples/Gwtdo.Sample.Test/Mapping/Tests.cs)

#### portuguese(pt-br)
```c#
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
```
See the pt-br test code [here](src/Samples/Gwtdo.Sample.Test/Mapping/PtBr/TestsPtBr.cs)


## Map the scenario
Copy your specification, paste it into your fixture test and map it using the extension methods.

```c#
public class Fixture : ScenarioFixture<Context>
{
    [Given("I have 100 shares of MSFT stock")]
    public void Have100SharesOfMsftStock() => Context.Stocks.Buy("MSFT", 100);

    [When("I ask to sell 20 shares of MSFT stock")]
    public void AskToSell20SharesOfMsftStock() => Context.Stocks.Sell("MSFT", 20);

    [Then("I should have 80 shares of MSFT stock")]
    public void ShouldHave80SharesOfMsftStock() => Context.Stocks.Shares["MSFT"].Should().Be(80);
}
```
See the complete code [here](src/Samples/Gwtdo.Sample.Test/Mapping/Fixture.cs)


## Using Let variable
Using Let the variable lazy loads only when it is used the first time in the test and get cached until that specific test is finished.

```c#
[Theory]
[InlineData(100, 20, 80, "MSFT")]
[InlineData(100, 50, 50, "APPL")]
[InlineData(100, 30, 70, "XYZW")]
public void sample_using_let(int share, int sells, int total, string asset)
{
    Let["share"] = share;
    Let["sells"] = sells;
    Let["total"] = total;
    Let["asset"] = asset;
    
    SCENARIO["User trades stocks"] =
        DESCRIBE
        | "User requests a sell before close of trading" |
        GIVEN
        | "I have :share shares of :asset stock" |
        WHEN
        | "I ask to sell :sells shares of :asset stock" |
        THEN
        | "I should have :total shares of :asset stock";
        
    SCENARIO.Execute().IsSuccess.Should().BeTrue();
}

//Mapping
//The cast method As<T> is provaided by FluentAssertions
public class Fixture : ScenarioFixture<Context>
{
    [Given("I have :share shares of :asset stock")]
    public void HaveDynamicSharesOfMsftStock() =>
        Context.Stocks.Buy(Let["asset"].As<string>(), Let["share"].As<int>());

    [When("I ask to sell :sells shares of :asset stock")]
    public void AskToSellDynamicSharesOfMsftStock() =>
        Context.Stocks.Sell(Let["asset"].As<string>(), Let["sells"].As<int>());

    [Then("I should have :total shares of :asset stock")]
    public void ShouldHaveDynamicSharesOfMsftStock() =>
        Context.Stocks.Shares[Let["asset"].As<string>()].Should().Be(Let["total"].As<int>());
}        
```

See the complete code [here](src/Samples/Gwtdo.Sample.Test/Mapping/Tests.cs).

## Executing
To execute the scenario you should use the method `SCENARIO.Execute()`. Remember validate the results after Scenario execution like this `SCENARIO.Execute().IsSuccess.Should().BeTrue();`.
The results should be success or fail. 

#### success result
```
USER TRADES STOCKS
------------------------------------------------------------
GIVEN
    I have 100 shares of MSFT stock
WHEN
    I ask to sell 20 shares of MSFT stock
THEN
    I should have 80 shares of MSFT stock
```

#### fail result

```shell
------------------------------------------------------------
GIVEN
    I have 100 shares of MSFT stock
    I have 150 shares of APPL stock
    The time is before close of trading
WHEN
    I ask to sell 20 shares of MSFT stock
THEN
    I should have 150 shares of APPL stock << Fail
------------------------------------------------------------
    Exception has been thrown by the target of an invocation.
    Expected Context.Stocks.Shares["APPL"] to be 110, but found 150.
------------------------------------------------------------
   at FluentAssertions.Execution.XUnit2TestFramework.Throw(String message)
   at FluentAssertions.Execution.TestFrameworkProvider.Throw(String message)
   ...
```

# Validating code with Specification Matching
Specification Matching is a set of features of the DSL `GWTDO` composed of two functionalities: 
- a) **the correspondence between scenarios and their mappings**; 
- b) and the **correspondence between the mapping and their function**. 

### Correspondence between specification and mapping

It is the function responsible for maintaining the integrity between scenarios and their mappings. 
Let's assume that developer ( bob 👨 ) changes the scenario `I have 100 shares of MSFT stock` to `I have 99 shares of MSFT stock`. 
Running this test results in a failure:

<p align="center">
    <img src="https://user-images.githubusercontent.com/357114/117551998-4a2efc00-b01f-11eb-9548-460644f5a193.png" />
</p>

In this result, the expression I have 99 shares of MSFT stock is highlighted with the value `(NOT MAPPED)`, indicating there isn't a mapping in fixture class for the scenario.


# Basic usage
If you prefer, you can only use code to write your specifications

#### scenario in wrote in C#

```c#
public class Tests : Feature<Context>, IClassFixture<Context>
{
    public Tests(Context context): base(context)
    {
        context.Setup();
    }

    [Fact]
    public void user_requests_a_sell()
    {
        GIVEN
            .I_have_100_shares_of_MSFT_stock();
        WHEN
            .I_ask_to_sell_20_shares_of_MSFT_stock();
        THEN
            .I_should_have_80_shares_of_MSFT_stock();
    }
}
```

#### Fixture file
```c#
public static class Fixture
{
    // GIVEN - ARRANGE
    public static Arrange<Context> I_have_100_shares_of_MSFT_stock(this Arrange<Context> fixtures) =>
        fixtures.Setup(f => f.Stocks.Buy("MSFT", 100));

    // WHEN - ACT
    public static void I_ask_to_sell_20_shares_of_MSFT_stock(this Act<Context> fixtures) =>
        fixtures.It(f => f.Stocks.Sell("MSFT", 20));

    // THEN - ASSERT
    public static Assert<Context> I_should_have_80_shares_of_MSFT_stock(this Assert<Context> fixtures) =>
        fixtures.Expect(x => x.Stocks.Shares["MSFT"].Should().Be(80));
}
```
        
See the complete code in the [Just Code sample](https://github.com/8T4/gwtdo/tree/main/src/Samples/Gwtdo.Sample.Test/JustCode).


# Guide to contributing to a GitHub project
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

- [[1] - Given-When-Then](https://martinfowler.com/bliki/GivenWhenThen.html)
- [[2] - Test fixture](https://en.wikipedia.org/wiki/Test_fixture)  
- [[3] - 3A – Arrange, Act, Assert](https://xp123.com/articles/3a-arrange-act-assert/)  
