﻿using System;
using Gwtdo.Scenarios;
using Gwtdo.Steps;

namespace Gwtdo;

/// <summary>
/// Although Given-When-Then style is symptomatic to BDD, the basic idea is pretty common when writing tests or
/// specification by example. Meszaros describes the pattern as Four-Phase Test. His four phases are Setup (Given),
/// Exercise (When), Verify (Then) and Teardown [5]. Bill Wake came up with the formulation as Arrange, Act, Assert.
/// <see href="https://martinfowler.com/bliki/GivenWhenThen.html"/>
/// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract partial class Feature<T> where T : IFeatureContext
{
    public string Id => Guid.NewGuid().ToString("N");
    
    protected Describe<T> DESCRIBE => Describe<T>.Create(this);
    protected Arrange<T> GIVEN => Arrange<T>.Create(Context);
    protected Act<T> WHEN => Act<T>.Create(Context);
    protected Assert<T> THEN => Assert<T>.Create(Context);
    protected And AND => And.Create();
    protected ScenarioVariables Let => SCENARIO.Let;

    protected T Context { get; set; }
    public Scenario<T> SCENARIO { get; private set; }

    protected Feature()
    {
    }

    protected Feature(T context) : this()
    {
        Context = context;
        SCENARIO = new Scenario<T>(string.Empty, context);
    }
}