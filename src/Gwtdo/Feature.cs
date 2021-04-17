using System;

namespace Gwtdo
{
    /// <summary>
    /// Although Given-When-Then style is symptomatic to BDD, the basic idea is pretty common when writing tests or
    /// specification by example. Meszaros describes the pattern as Four-Phase Test. His four phases are Setup (Given),
    /// Exercise (When), Verify (Then) and Teardown [5]. Bill Wake came up with the formulation as Arrange, Act, Assert.
    /// <see href="https://martinfowler.com/bliki/GivenWhenThen.html"/>
    /// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Feature<T> where T : IFixture
    {
        protected T Fixture { get; set; }
        
        protected Arrange<T> Given => Arrange<T>.Create(Fixture);
        protected Act<T> When => Act<T>.Create(Fixture);
        protected Assert<T> Then => Assert<T>.Create(Fixture);

        public Feature()
        {
            
        }

        protected Feature(T fixture)
        {
            Fixture = fixture;
        }
    }
}