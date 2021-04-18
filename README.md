<p align="center">
    <img src="https://raw.githubusercontent.com/8T4/gwtdo/main/doc/img/logo.png" />
</p>

**GWTdo is a .NET library that helps developers write readable tests**.
It's a DSL based on the [Given-When-Then](https://martinfowler.com/bliki/GivenWhenThen.html) style which could be used in your test projects.

[![.NET](https://github.com/8T4/gwtdo/actions/workflows/dotnet.yml/badge.svg)](https://github.com/8T4/gwtdo/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/8T4/gwtdo/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/8T4/gwtdo/actions/workflows/codeql-analysis.yml)

# Getting Started

## Instalation
This package is available through Nuget Packages: ( 🇬🇧 ) https://www.nuget.org/packages/Gwtdo ou ( 🇧🇷 ) https://www.nuget.org/packages/Gwtdo.PtBr

| Package |  Version | Downloads | Maintainability |
| ------- | ----- | ----- |----- |
| 🇬🇧 `GWTdo` | [![NuGet](https://img.shields.io/nuget/v/Gwtdo.svg)](https://www.nuget.org/packages/Gwtdo) | [![Nuget](https://img.shields.io/nuget/dt/Gwtdo.svg)](https://www.nuget.org/packages/Gwtdo) | [![Codacy Badge](https://app.codacy.com/project/badge/Grade/51e1962835f24f65a3813d078061a9ef)](https://www.codacy.com/gh/8T4/gwtdo/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=8T4/gwtdo&amp;utm_campaign=Badge_Grade) |
| 🇧🇷 `GWTdo.PtBr` | [![NuGet](https://img.shields.io/nuget/v/Gwtdo.PtBr.svg)](https://www.nuget.org/packages/Gwtdo.PtBr) | [![Nuget](https://img.shields.io/nuget/dt/Gwtdo.PtBr.svg)](https://www.nuget.org/packages/Gwtdo.PtBr) | [![Codacy Badge](https://app.codacy.com/project/badge/Grade/51e1962835f24f65a3813d078061a9ef)](https://www.codacy.com/gh/8T4/gwtdo/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=8T4/gwtdo&amp;utm_campaign=Badge_Grade) |

## Example
In our demonstration, we want to test class `Stock.cs`([see the code](src/Gwtdo.Sample/Stocks/Stock.cs)) and make sure it covers the following specification. 

```yaml
Feature: User trades stocks
  Scenario: User requests a sell before close of trading
    Given I have 100 shares of MSFT stock
    When I ask to sell 20 shares of MSFT stock
    Then I should have 80 shares of MSFT stock
```

<p align="center"><b>🇧🇷 "Versão brasileira..."</b></p>

```yaml
Feature: Transacoes Na Bolsa Pelo Usuario
  Scenario: Usuario requisitando venda de acao
    Dado Que eu tenho 100 acoes MSFT
    Quando solicito a venda de 20 acoes MSFT
    Entao Eu devo ter 80 acoes MSFT
```

Sessions "Test fixture", "Extension methods" and "Test" show how to implements this specification.

### Test fixture 
A test fixture is an environment used to consistently test some item, device, or piece of software. [[2]]()
In our example, the `StockFixture` is just a simple `record` ([C# 9.0](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#record-types)) type that contains a
`Stock` instance as a property. All fixtures using `GWTdo` should have implemented `IFixture` interface.

![image](https://user-images.githubusercontent.com/357114/115149792-16723f00-a03c-11eb-8bbe-0685e15e76c4.png)

### Extension methods

#### Alias directive
Use [alias directive](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive) 
to make your extension methods more readable.

![image](https://user-images.githubusercontent.com/357114/115149879-6f41d780-a03c-11eb-859e-b04181d070bc.png)

<p align="center"><b>🇧🇷 "Versão brasileira..."</b></p>

![image](https://user-images.githubusercontent.com/357114/115150857-8e426880-a040-11eb-8454-243888cd1170.png)


#### Arrange
Set up the object to be tested. We may need to surround the object with collaborators. For testing purposes, those collaborators might be test objects (mocks, fakes, etc.) or the real thing.
[[3]](https://xp123.com/articles/3a-arrange-act-assert/)

![image](https://user-images.githubusercontent.com/357114/115150066-2b030700-a03d-11eb-8442-bc4eb64b670b.png)

<p align="center"><b>🇧🇷 "Versão brasileira..."</b></p>

![image](https://user-images.githubusercontent.com/357114/115150839-79fe6b80-a040-11eb-9d59-f12a9347e2ea.png)


#### Act
Act on the object (through some mutator). You may need to give it parameters (again, possibly test objects).
[[3]](https://xp123.com/articles/3a-arrange-act-assert/)

![image](https://user-images.githubusercontent.com/357114/115150212-d57b2a00-a03d-11eb-8939-2933b68bc3d5.png)

<p align="center"><b>🇧🇷 "Versão brasileira..."</b></p>

![image](https://user-images.githubusercontent.com/357114/115150807-66eb9b80-a040-11eb-8975-b0c546486226.png)


#### Assert
Make claims about the object, its collaborators, its parameters, and possibly (rarely!!) global state.
[[3]](https://xp123.com/articles/3a-arrange-act-assert/)

![image](https://user-images.githubusercontent.com/357114/115150366-605c2480-a03e-11eb-8b56-cf33f64beae6.png)

<p align="center"><b>🇧🇷 "Versão brasileira..."</b></p>

![image](https://user-images.githubusercontent.com/357114/115150788-4cb1bd80-a040-11eb-9e74-1317073b046a.png)


### Test
Now we are ready to test our code using the `StockFixture` and their extension methods (`Arrange`, `Act` and `Assert`).
For this, you should extend the `Feature<T>` class and instantiate the fixture in their `Fixture` property.

#### basic usage

![image](https://user-images.githubusercontent.com/357114/115150548-3e16d680-a03f-11eb-81a2-afd5bf38c8ea.png)

<p align="center"><b>🇧🇷 "Versão brasileira..."</b></p>

![image](https://user-images.githubusercontent.com/357114/115150699-e88ef980-a03f-11eb-9afe-99c705ad96b0.png)



#### example (3) - A little bit complex

![image](https://user-images.githubusercontent.com/357114/115150606-8209db80-a03f-11eb-9921-e78b28c1a9e2.png)

<p align="center"><b>🇧🇷 "Versão brasileira..."</b></p>

![image](https://user-images.githubusercontent.com/357114/115150659-c2695980-a03f-11eb-99f5-7b73f4575c5a.png)


See those examples in the [sample 🇬🇧](src/Gwtdo.Sample.Test/Stocks) project ou, se preferir, veja nossa versão [brazuca 🇧🇷](src/Gwtdo.Sample.PtBr.Test/Stocks)

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
