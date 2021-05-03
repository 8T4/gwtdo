using System;
using System.Data;
using Gwtdo.Extensions;

namespace Gwtdo.Scenarios
{
    /// <summary>
    /// Use this class to Map Scenario Expression To Actions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract partial class ScenarioMapper<T> where T : IFixture
    {
        private readonly T _fixture;

        public Scenario<T> SCENARIO { get; set; }
        protected ScenarioMapper<T> DESCRIBE => this;
        protected Arrange<T> GIVEN => Arrange<T>.Create(_fixture);
        protected Act<T> WHEN => Act<T>.Create(_fixture);
        protected Assert<T> THEN => Assert<T>.Create(_fixture);

        protected And AND => And.Create();


        public ScenarioMapper(Scenario<T> scenario, T fixture)
        {
            SCENARIO = scenario;
            _fixture = fixture;
        }
    }

    /// <summary>
    /// OPERATORS
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract partial class ScenarioMapper<T> where T : IFixture
    {
        public static ScenarioMapper<T> operator |(ScenarioMapper<T> mapper, (string value, Action<T> method) action)
        {
            var syntagma = new Syntagma<T>(action.value, action.method);
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
        public static ScenarioMapper<T> operator |(ScenarioMapper<T> mapper, ScenarioMapper<T> other) => Add(mapper, "AND");
        public static ScenarioMapper<T> operator |(ScenarioMapper<T> mapper, Arrange<T> other) => Add(mapper, "GIVEN");
        public static ScenarioMapper<T> operator |(ScenarioMapper<T> mapper, Act<T> other) => Add(mapper, "WHEN");
        public static ScenarioMapper<T> operator |(ScenarioMapper<T> mapper, Assert<T> other) => Add(mapper, "THEN");
        
        private static ScenarioMapper<T> Add(ScenarioMapper<T> mapper, string value)
        {
            var syntagma = new Syntagma<T>(value, null);
            mapper.SCENARIO.MappedParadigms.AddSyntagma(syntagma);
            return mapper;
        }            
    }
}