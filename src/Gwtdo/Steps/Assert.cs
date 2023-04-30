using System;

namespace Gwtdo.Steps;

/// <summary>
/// Make claims about the object, its collaborators, its parameters, and possibly (rarely!!) global state. 
/// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
/// </summary>
/// <typeparam name="T"></typeparam>    
public sealed class Assert<T> : Step<T> where T : class
{
    public Assert<T> And => this;

    private Assert(Feature<T> value) : base(value)
    {
    }

    public static Assert<T> Create(Feature<T> value) => new(value);

    public Assert<T> Expect(Action<T> action)
    {
        action.Invoke(Value);
        return this;
    }
}