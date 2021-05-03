﻿using Gwtdo.Linguistic;
using Gwtdo.Scenarios;

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
    public abstract partial class Feature<T> where T : IFixture
    {
        protected T Fixture { get; set; }
        protected Arrange<T> Given => Arrange<T>.Create(Fixture);
        protected Act<T> When => Act<T>.Create(Fixture);
        protected Assert<T> Then => Assert<T>.Create(Fixture);

        public Scenario<T> SCENARIO { get; set; }
        protected Feature<T> DESCRIBE => this;
        protected Arrange<T> GIVEN => Arrange<T>.Create(Fixture);
        protected Act<T> WHEN => Act<T>.Create(Fixture);
        protected Assert<T> THEN => Assert<T>.Create(Fixture);
        protected And AND => And.Create();

        protected Feature()
        {
        }

        protected Feature(T fixture)
        {
            Fixture = fixture;
            SCENARIO = new Scenario<T>(string.Empty, fixture);
        }
    }

    /// <summary>
    /// OPERATORS
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract partial class Feature<T> where T : IFixture
    {
        public static Feature<T> operator |(Feature<T> feature, string other)
        {
            var syntagma = new Syntagma<T>(other, null);
            
            if (!feature.SCENARIO.Paradigms.SyntagmaExists(syntagma))
            {
                feature.SCENARIO.Paradigms.AddSyntagma(syntagma);
            }

            return feature;
        }

        public static Feature<T> operator |(Feature<T> feature, And other) => feature;
        public static Feature<T> operator |(Feature<T> feature, Arrange<T> other) => Add(feature, "GIVEN");
        public static Feature<T> operator |(Feature<T> feature, Act<T> other) => Add(feature, "WHEN");
        public static Feature<T> operator |(Feature<T> feature, Assert<T> other) => Add(feature, "THEN");
        
        private static Feature<T> Add(Feature<T> feature, string value)
        {
            var syntagma = new Syntagma<T>(value, null);
            feature.SCENARIO.Paradigms.AddSyntagma(syntagma);
            return feature;
        }         
    }
}