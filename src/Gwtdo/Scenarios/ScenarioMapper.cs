using System;
using Gwtdo.Linguistic;

namespace Gwtdo.Scenarios
{
    /// <summary>
    /// Use this class to Map Scenario Expression To Actions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract partial class ScenarioMapper<T> where T : IFixture
    {
        protected T Fixture { get; }

        public Scenario<T> SCENARIO { get; }
        protected ScenarioMapper<T> DESCRIBE => this;
        protected Arrange<T> GIVEN => Arrange<T>.Create(Fixture);
        protected Act<T> WHEN => Act<T>.Create(Fixture);
        protected Assert<T> THEN => Assert<T>.Create(Fixture);
        protected And AND => And.Create();

        protected ScenarioMapper(Scenario<T> scenario, T fixture)
        {
            SCENARIO = scenario;
            Fixture = fixture;
        }

        protected static Action<T> DefaultAction => (f) => { };
    }

    /// <summary>
    /// Mapper OPERATORS
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract partial class ScenarioMapper<T> where T : IFixture
    {
        public static ScenarioMapper<T> operator |(ScenarioMapper<T> mapper, (string value, Action<T> method) action)
        {
            var (value, method) = action;
            var syntagma = new Syntagma<T>(value, method);
            mapper.SCENARIO.MappedParadigms.AddSyntagma(syntagma);
            return mapper;
        }

        public static ScenarioMapper<T> operator |(ScenarioMapper<T> mapper, string value)
        {
            var syntagma = new Syntagma<T>(value, null);
            mapper.SCENARIO.MappedParadigms.AddSyntagma(syntagma);
            return mapper;
        }

        public static ScenarioMapper<T> operator |(ScenarioMapper<T> mapper, (string value, Action) action) => mapper;
        public static ScenarioMapper<T> operator |(ScenarioMapper<T> mapper, And other) => mapper;
        public static ScenarioMapper<T> operator |(ScenarioMapper<T> mapper, ScenarioMapper<T> other) => Add(mapper, And.Name);
        public static ScenarioMapper<T> operator |(ScenarioMapper<T> mapper, Arrange<T> other) => Add(mapper, Arrange.Name);
        public static ScenarioMapper<T> operator |(ScenarioMapper<T> mapper, Act<T> other) => Add(mapper, Act.Name);
        public static ScenarioMapper<T> operator |(ScenarioMapper<T> mapper, Assert<T> other) => Add(mapper, Assert.Name);
        
        private static ScenarioMapper<T> Add(ScenarioMapper<T> mapper, string value)
        {
            var syntagma = new Syntagma<T>(value, null);
            mapper.SCENARIO.MappedParadigms.AddSyntagma(syntagma);
            return mapper;
        }            
    }
}