namespace Gwtdo
{
    /// <summary>
    /// This class represents the "Setup" phase (Given)
    /// <see href="https://martinfowler.com/bliki/GivenWhenThen.html"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Setup<T> where T : IFixture
    {
        public T Value { get; }
        public Setup<T> And =>  this;
        
        private Setup(T value) => Value = value;
        public static Setup<T> Create(T value) => new Setup<T>(value);
    }
}