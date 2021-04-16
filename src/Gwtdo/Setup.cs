namespace Gwtdo
{
    public sealed class Setup<T> where T : IFixture
    {
        public T Value { get; }
        public Setup<T> And =>  this;
        
        private Setup(T value) => Value = value;
        public static Setup<T> Create(T value) => new Setup<T>(value);
    }
}