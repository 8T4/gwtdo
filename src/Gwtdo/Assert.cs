using System;

namespace Gwtdo
{
    /// <summary>
    /// Make claims about the object, its collaborators, its parameters, and possibly (rarely!!) global state. 
    /// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>    
    public sealed class Assert<T> where T : IFixture
    {
        private T Value { get; }
        public Assert<T> And => this;

        private Assert(T value)
        {
            Value = value;
        }

        internal static Assert<T> Create(T value) => new Assert<T>(value);     
        
        public Assert<T> Verify(Action<T> action) => Expect(action);
        
        public Assert<T> Expect(Action<T> action)
        {
            action.Invoke(Value);
            return this;
        }
    }

    internal static class Assert
    {
        public static string Name => $"\u001b[36;1mTHEN\u001b[0m";
    }
}