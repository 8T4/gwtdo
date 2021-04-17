namespace Gwtdo
{
    /// <summary>
    /// This class represents the "Verify" phase (Then)
    /// <see href="https://martinfowler.com/bliki/GivenWhenThen.html"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>    
    public sealed class Verify<T> where T: IFixture
    {
        public T Value { get; }
        public Verify<T> And =>  this;
        
        private Verify(T value) => Value = value;
        public static Verify<T> Create(T value) => new Verify<T>(value);          
    }
}