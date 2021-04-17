namespace Gwtdo
{
    /// <summary>
    /// Make claims about the object, its collaborators, its parameters, and possibly (rarely!!) global state. 
    /// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>    
    public sealed class Assert<T> where T: IFixture
    {
        public T Value { get; }
        public Assert<T> And =>  this;
        
        private Assert(T value) => Value = value;
        public static Assert<T> Create(T value) => new Assert<T>(value);          
    }
}