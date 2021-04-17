namespace Gwtdo
{
    /// <summary>
    /// Act on the object (through some mutator). You may need to give it parameters (again, possibly test objects).
    /// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Act<T> where T: IFixture
    {
        public T Value { get; }
        public Act<T> And =>  this;
        
        private Act(T value) => Value = value;
        public static Act<T> Create(T value) => new Act<T>(value);        
    }
}