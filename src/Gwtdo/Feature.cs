using System;

namespace Gwtdo
{
    public abstract class Feature<T> where T : IFixture
    {
        protected T Fixture { get; }
        
        protected Setup<T> Given => Setup<T>.Create(Fixture);
        protected Exercise<T> When => Exercise<T>.Create(Fixture);
        protected Verify<T> Then => Verify<T>.Create(Fixture);

        protected Feature(T fixture)
        {
            Fixture = fixture;
        }
    }
}