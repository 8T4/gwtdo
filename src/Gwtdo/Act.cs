using System;

namespace Gwtdo
{
    /// <summary>
    /// Act on the object (through some mutator). You may need to give it parameters (again, possibly test objects).
    /// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Act<T> where T : IFixture
    {
        private T Value { get; }
        public Act<T> And => this;

        private Act(T value)
        {
            Value = value;
        }

        internal static Act<T> Create(T value) => new Act<T>(value);

        [Obsolete("Use It()")]
        public Act<T> Excecute(Action<T> action) => It(action);
        
        public Act<T> It(Action<T> action)
        {
            action.Invoke(Value);
            return this;
        }        
    }
    
    internal static class Act
    {
        public static string Name => $"\u001b[36;1mWHEN\u001b[0m";
    }    
}