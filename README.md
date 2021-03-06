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

## Write the specification
Specify your test using natural language within a C # method. Easy, simple and fast.
```c#
[Fact]
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
See the complete code [here](https://github.com/8T4/gwtdo/blob/main/src/Samples/Gwtdo.Sample.Test/NaturalLanguange/Tests.cs)

## Mapping the specification
Copy your specification, paste it into your fixture test and map it using the extension methods.

```c#
public void Setup_user_trades_stocks_scenario()
{
    SCENARIO["User trades stocks"] =
        DESCRIBE | "User requests a sell before close of trading" |
           GIVEN | "I have 100 shares of MSFT stock".MapAction(Have100SharesOfMsft) |
            WHEN | "I ask to sell 20 shares of MSFT stock".MapAction(AskToSell20SharesOfMsft) |
            THEN | "I should have 80 shares of MSFT stock".MapAction(ShouldHave80SharesOfMsft);
}


private static Action<StockFixture> Have100SharesOfMsft => 
    f => f.Stocks.Buy("MSFT", 100);
    
private static Action<StockFixture> AskToSell20SharesOfMsft => 
    f => f.Stocks.Sell("MSFT", 20);    

private static Action<StockFixture> ShouldHave80SharesOfMsft => 
     f => f.Stocks.Shares["MSFT"].Should().Be(80);     

```
See the complete code [here](https://github.com/8T4/gwtdo/blob/main/src/Samples/Gwtdo.Sample.Test/NaturalLanguange/Fixture.cs)


## Using Let variable
Using Let the variable lazy loads only when it is used the first time in the test and get cached until that specific test is finished.

```c#
[Theory]
[InlineData(100, 20, 80)]
[InlineData(100, 50, 50)]
[InlineData(100, 30, 70)]
public void user_requests_a_sell_dynamic(int share, int sells, int total)
{
    Let["share-value"] = share;
    Let["sells-value"] = sells;
    Let["total-value"] = total;
    
    SCENARIO["User trades stocks"] =
        DESCRIBE | "User requests a sell before close of trading" |
           GIVEN | "I have :share-value shares of MSFT stock" |
            WHEN | "I ask to sell :sells-value shares of MSFT stock" |
            THEN | "I should have :total-value shares of MSFT stock";
        
    ...
}

//Mapping
public void Setup_user_trades_stocks_scenario_dynamic()
{
    SCENARIO["User trades stocks"] =
        DESCRIBE | "User requests a sell before close of trading" |
           GIVEN | "I have :share-value shares of MSFT stock".MapAction(HaveDynamicSharesOfMsftStock) |
            WHEN | "I ask to sell :sells-value shares of MSFT stock".MapAction(AskToSellDynamicSharesOfMsftStock) |
            THEN | "I should have :total-value shares of MSFT stock".MapAction(ShouldHaveDynamicSharesOfMsftStock);
} 

private Action<StockFixture> HaveDynamicSharesOfMsftStock =>
    f => f.Stocks.Buy("MSFT", Let.Get<int>("share-value"));
    
private Action<StockFixture> AskToSellDynamicSharesOfMsftStock =>
    f => f.Stocks.Sell("MSFT", Let.Get<int>("sells-value"));     
    
private Action<StockFixture> ShouldHaveDynamicSharesOfMsftStock =>
    f => f.Stocks.Shares["MSFT"].Should().Be(Let.Get<int>("total-value"));
```

See the complete code [here](https://github.com/8T4/gwtdo/tree/main/src/Samples/Gwtdo.Sample.Test/NaturalLanguange).

# Validating code with Specification Matching
Specifiation Matching is a set of features of the DSL `GWTDO` composed of two functionalities: a) **the correspondence between specification and mapping**; b) and the **correspondence between the mapping and the function**. 

## Correspondence between specification and mapping

It is the function responsible for maintaining the integrity between the specification and the mapping. Let's assume that developer ( bob 👨 ) changes the code `I have 100 shares of MSFT stock` to `I have 99 shares of MSFT stock`. Running this test results in a failure:

<p align="center">
    <img src="https://user-images.githubusercontent.com/357114/117551998-4a2efc00-b01f-11eb-9548-460644f5a193.png" />
</p>

In this result, the expression I have 99 shares of MSFT stock is highlighted with the value (NOT MAPPED), indicating that it has not been mapped.
Now, imagine that developer ( alice 👩 ) adds a little more complexity to your test and adds the expression `AND | I have 150 shares of APPL stock` in the specification, without failing to map it. When running the test, we will have the following result:

<p align="center">
    <img src="https://user-images.githubusercontent.com/357114/117552124-025ca480-b020-11eb-8a09-a8e0779c65e4.png" />
</p>

## Correspondence between mapping expression and a function:
It is the function of the mapping class that allows the integration between the expression and the test code, through the call to the `MapAction()` method. This method is responsible for satisfying the correctness formulae `{ X => Y | Y = f:P A Q }`, as the following codes illustrate:

```c#

WHEN | "I ask to sell 20 shares of MSFT stock".MapAction(AskToSell20SharesOfMsft)
...

private static Action<StockFixture> AskToSell20SharesOfMsft => (f) => f.Stocks.Sell("MSFT", 20);    
```

# Just use C#

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
