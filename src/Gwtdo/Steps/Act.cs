using System;

namespace Gwtdo.Steps;

/// <summary>
/// This is a C# code that defines a sealed generic class named Act<T> that inherits from Step<T> class. The T generic
/// type parameter is constrained to be a reference type.
/// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class Act<T> : Step<T> where T : class
{
    /// <summary>
    /// The And property is a read-only property of type Act<T> that returns the current instance of Act<T>.
    /// This allows for method chaining to build a fluent interface.
    /// </summary>
    public Act<T> And => this;

    /// <summary>
    /// The private constructor takes a Feature<T> object and calls the base constructor of Step<T> with the same
    /// Feature<T> object.
    /// </summary>
    /// <param name="value"></param>
    private Act(Feature<T> value) : base(value)
    {
    }

    /// <summary>
    /// The Create static method is a factory method that takes a Feature<T> object and returns a new instance of the
    /// Act<T> class with the same Feature<T> object. This is useful to create an instance of the Act<T> class in a
    /// concise way.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Act<T> Create(Feature<T> value) => new(value);

    /// <summary>
    /// The It method takes an Action<T> delegate as a parameter and invokes it with the Value property (of type T) of
    /// the Feature<T> object. Then it returns the current instance of Act<T>. This method is used to execute an action
    /// on the Feature<T> object.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public Act<T> It(Action<T> action)
    {
        action.Invoke(Value);
        return this;
    }
}