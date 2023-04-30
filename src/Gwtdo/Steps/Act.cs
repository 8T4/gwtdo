using System;

namespace Gwtdo.Steps;

/// <summary>
/// Act on the object (through some mutator). You may need to give it parameters (again, possibly test objects).
/// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class Act<T> : Step<T> where T : class
{
    public Act<T> And => this;

    private Act(Feature<T> value) : base(value)
    {
    }

    public static Act<T> Create(Feature<T> value) => new(value);

    public Act<T> It(Action<T> action)
    {
        action.Invoke(Value);
        return this;
    }
}