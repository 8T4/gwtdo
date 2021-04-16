namespace Gwtdo
{
    public sealed class Verify<T> where T: IFixture
    {
        public T Value { get; }
        public Verify<T> And =>  this;
        
        private Verify(T value) => Value = value;
        public static Verify<T> Create(T value) => new Verify<T>(value);          
    }
}