using System;
namespace Gwtdo
{
    /// <summary>
    /// Set up the object to be tested. We may need to surround the object with collaborators.
    /// For testing purposes, those collaborators might be test objects (mocks, fakes, etc.) or the real thing.
    /// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Arrange<T>  where T : IFixture
    {
        private T Value { get; }
        public Arrange<T> And => this;
        public static string Name => "GIVEN";
        
        private Arrange(T value)
        {
            Value = value;
        }

        internal static Arrange<T> Create(T value) => new Arrange<T>(value);        
        
        /// <summary>
        /// Use setup to initialize actions that are eager loaded to test your specs.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Arrange<T> Setup(Action<T> action)
        {
            action.Invoke(Value);
            return this;
        }
    }
}