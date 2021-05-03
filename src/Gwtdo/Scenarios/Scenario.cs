using System;
using System.Linq;
using System.Text;
using Gwtdo.Extensions;
using Gwtdo.Linguistic;

namespace Gwtdo.Scenarios
{
    public class Scenario<T> where T : IFixture
    {
        private readonly T _fixture;
        private string Description { get; set; }
        internal Paradigm<T> Paradigms { get; }
        internal Paradigm<T> MappedParadigms { get; }
        public Action<string> RedirectStandardOutput { get; set; }

        public Scenario<T> this[string description]
        {
            get => this;
            set
            {
                value = this;
                value.Description = description;
            }
        }

        public Scenario(string description, T fixture)
        {
            Paradigms = description;
            MappedParadigms = description;
            _fixture = fixture;
        }

        public Scenario(string description)
        {
            Paradigms = description;
            MappedParadigms = description;
        }

        public ScenarioResult Execute()
        {
            var isValidScenario = AllScenarioWereMapped();

            return isValidScenario.IsFailure
                ? isValidScenario
                : ExectueMappedParadigms();
        }

        private ScenarioResult AllScenarioWereMapped()
        {
            var result = new StringBuilder();
            result.AppendLine();

            if (!MappedParadigms.Syntagmas.Any())
            {
                result.Insert(0, $"\u001b[0m\u001b[33mthe SCENARIO \"{this.Description.ToUpper()}\" SHOULD BE MAPPED\u001b[0m");
                return ScenarioResult.Fail(result.ToString());
            }

            var allExpressionMapped = true;

            foreach (var (_, value) in Paradigms.Syntagmas)
            {
                if (!MappedParadigms.SyntagmaExists(value))
                {
                    allExpressionMapped = false;
                    result.AppendLine($"\u001b[0m\u001b[31m{value.Metalanguage.Sign.Signifier.Value}\u001b[0m\u001b[31;1m (NOT MAPPED)\u001b[0m".Indent());
                    continue;
                }

                result.AppendLine($"{value.Metalanguage.Sign.Signifier.Value}".Indent());
            }

            return allExpressionMapped
                ? ScenarioResult.Ok(result.ToString())
                : ScenarioResult.Fail(result.ToString());
        }

        private ScenarioResult ExectueMappedParadigms()
        {
            var result = new StringBuilder();
            result.AppendLine();
            
            foreach (var (key, value) in Paradigms.Syntagmas)
            {
                try
                {
                    var mapped = MappedParadigms.GetSyntagma(key);

                    if (mapped.Sign.Signified.Value == null)
                    {
                        result.AppendLine($"{mapped.Metalanguage.Sign.Signifier.Value}");
                        continue;
                    }

                    mapped.Sign.Signified.Value.Invoke(_fixture);
                    result.AppendLine($"{mapped.Metalanguage.Sign.Signifier.Value}".Indent(4));
                }
                catch
                {
                    result.AppendLine($"\u001b[0m\u001b[31m{value.Metalanguage.Sign.Signifier.Value}\u001b[0m".Indent(4));
                    result.Insert(0, $"\u001b[0m\u001b[31m{Description.ToUpper()}\u001b[0m");
                    return ScenarioResult.Fail(result.ToString());
                }
            }

            result.Insert(0, $"\u001b[0m\u001b[32m{this.Description.ToUpper()}\u001b[0m");
            RedirectStandardOutput?.Invoke(result.ToString());
            
            return ScenarioResult.Ok(result.ToString());
        }

        public static implicit operator Scenario<T>(Feature<T> feature)
        {
            return feature.SCENARIO;
        }

        public static implicit operator Scenario<T>(ScenarioMapper<T> feature)
        {
            return feature.SCENARIO;
        }
    }
}