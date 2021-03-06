using System;
using Gwtdo.Linguistic;
using Gwtdo.Scenarios;

namespace Gwtdo.Localizations
{
    /// <summary>
    /// Although Given-When-Then style is symptomatic to BDD, the basic idea is pretty common when writing tests or
    /// specification by example. Meszaros describes the pattern as Four-Phase Test. His four phases are Setup (Given),
    /// Exercise (When), Verify (Then) and Teardown [5]. Bill Wake came up with the formulation as Arrange, Act, Assert.
    /// <see href="https://martinfowler.com/bliki/GivenWhenThen.html"/>
    /// <see href="https://xp123.com/articles/3a-arrange-act-assert/"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract partial class FeaturePtBr<T> where T : IFixture
    {
        protected FeaturePtBr<T> DESCREVA => this;
        protected Arrange<T> DADO => Arrange<T>.Create(Fixture);
        protected Act<T> QUANDO => Act<T>.Create(Fixture);
        protected Assert<T> ENTAO => Assert<T>.Create(Fixture);
        protected And E => And.Create();

        protected FeatureVariables FeatureVariables
        {
            get => CENARIO.FeatureVariables;
            set => CENARIO.FeatureVariables = value;
        }
        
        private T Fixture { get; set; }
        public Scenario<T> CENARIO { get; private set; }

        protected FeaturePtBr()
        {
        }

        protected FeaturePtBr(T fixture) : this()
        {
            Fixture = fixture;
            CENARIO = new Scenario<T>(string.Empty, fixture);
        }

        protected void SetFixture(T fixture)
        {
            Fixture = fixture;
            CENARIO = new Scenario<T>(string.Empty, fixture);            
        }
    }

    /// <summary>
    /// Feature OPERATORS
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract partial class FeaturePtBr<T> where T : IFixture
    {
        public static FeaturePtBr<T> operator |(FeaturePtBr<T> feature, string other)
        {
            var syntagma = new Syntagma<T>(other, null);
            
            if (!feature.CENARIO.Paradigms.SyntagmaExists(syntagma))
            {
                feature.CENARIO.Paradigms.AddSyntagma(syntagma);
            }

            return feature;
        }
        
        public static FeaturePtBr<T> operator |(FeaturePtBr<T> feature, And other) => feature;
        public static FeaturePtBr<T> operator |(FeaturePtBr<T> feature, FeatureVariables other) => feature;
        public static FeaturePtBr<T> operator |(FeaturePtBr<T> feature, Arrange<T> other) => Add(feature, Arrange.Name);
        public static FeaturePtBr<T> operator |(FeaturePtBr<T> feature, Act<T> other) => Add(feature, Act.Name);
        public static FeaturePtBr<T> operator |(FeaturePtBr<T> feature, Assert<T> other) => Add(feature, Assert.Name);
        
        private static FeaturePtBr<T> Add(FeaturePtBr<T> feature, string value)
        {
            var syntagma = new Syntagma<T>(value, null);
            feature.CENARIO.Paradigms.AddSyntagma(syntagma);
            return feature;
        }         
    }
}