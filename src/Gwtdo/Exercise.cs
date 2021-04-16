namespace Gwtdo
{
    public sealed class Exercise<T> where T: IFixture
    {
        public T Value { get; }
        public Exercise<T> And =>  this;
        
        private Exercise(T value) => Value = value;
        public static Exercise<T> Create(T value) => new Exercise<T>(value);        
    }
}