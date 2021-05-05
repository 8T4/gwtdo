using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Gwtdo.Extensions;
using Gwtdo.Linguistic;

namespace Gwtdo.Scenarios
{
    /// <summary>
    /// Scenario
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Scenario<T> where T : IFixture
    {
        private readonly T _fixture;
        private string Description { get; set; }
        internal Paradigm<T> Paradigms { get; }
        internal Paradigm<T> MappedParadigms { get; }
        public Action<string> RedirectStandardOutput { get; set; }

        public Scenario<T> this[string description]
        {
            get => this;
            set => value.Description = description;
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

        public static implicit operator Scenario<T>(Feature<T> feature)
        {
            return feature.SCENARIO;
        }

        public static implicit operator Scenario<T>(ScenarioMapper<T> feature)
        {
            return feature.SCENARIO;
        }
    }

    /// <summary>
    /// Scenario Validation and Execution methods
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Scenario<T> where T : IFixture
    {
        public ScenarioResult Execute()
        {
            var mappedParadigmsEmpity = VerifyIfMappedParadigmsIsNotEmpity();
            if (mappedParadigmsEmpity.IsFailure)
            {
                return mappedParadigmsEmpity;
            }
            
            var allScenarioWereMapped = VerifyIfAllScenarioWereMapped();
            if (allScenarioWereMapped.IsFailure)
            {
                return allScenarioWereMapped;
            }            

            var result = ExectueMappedParadigms();
            Console.WriteLine(result.ToString());
            RedirectStandardOutput?.Invoke(result.ToString());
            return result;
        }

        private ScenarioResult ExectueMappedParadigms()
        {
            var result = new StringBuilder();
            result.AppendLine("...");
            result.AppendHorizontalLine(60);
            
            foreach (var (key, value) in Paradigms.Syntagmas)
            {
                try
                {
                    var mapped = MappedParadigms.GetSyntagma(key);

                    if (mapped.Sign.Signified.Value == null)
                    {
                        result.AppendLine(mapped.Metalanguage.Sign.Signifier.Value);
                        continue;
                    }

                    mapped.Sign.Signified.Value.Invoke(_fixture);
                    result.AppendLine($"{mapped.Metalanguage.Sign.Signifier.Value}".Indent(4));
                }
                catch(Exception ex)
                {
                    result.AppendLine(Colors.Error(value.Metalanguage.Sign.Signifier.Value.Indent(4)));
                    result.AppendLine(Colors.Error(ex.Message.Indent(4)));
                    result.Insert(0, Colors.Error(Description.ToUpper(CultureInfo.InvariantCulture)));

                    return ScenarioResult.Fail(result.ToString());
                }
            }

            result.Insert(0, Colors.Success(Description.ToUpper(CultureInfo.InvariantCulture)));
            return ScenarioResult.Ok(result.ToString());
        }

        private ScenarioResult VerifyIfMappedParadigmsIsNotEmpity()
        {
            var result = new StringBuilder();
            AppendScenarioDescription(ref result);

            if (MappedParadigms.IsNotEmpty)
            {
                return ScenarioResult.Ok();
            }
            
            result.Insert(0, Colors.Warning($"the SCENARIO \"{Description.ToUpper(CultureInfo.InvariantCulture)}\" SHOULD BE MAPPED"));
            return ScenarioResult.Fail(result.ToString());
        }
        
        private ScenarioResult VerifyIfAllScenarioWereMapped()
        {
            var allExpressionMapped = true;
            
            var result = new StringBuilder();
            AppendScenarioDescription(ref result);

            foreach (var (key, value) in Paradigms.Syntagmas)
            {
                if (!MappedParadigms.SyntagmaExists(value))
                {
                    allExpressionMapped = false;
                    result.AppendLine($"{Colors.Reset(value.Metalanguage.Sign.Signifier.Value)} {Colors.Error("(NOT MAPPED)")}".Indent());
                    continue;
                }

                var mapped = MappedParadigms.GetSyntagma(key);
                
                if (mapped.Sign.Signified.Value == null)
                {
                    result.AppendLine($"{value.Metalanguage.Sign.Signifier.Value}");
                    continue;
                }                  

                result.AppendLine($"{value.Metalanguage.Sign.Signifier.Value}".Indent());
            }

            return allExpressionMapped
                ? ScenarioResult.Ok(result.ToString())
                : ScenarioResult.Fail(result.ToString());
        }

        private void AppendScenarioDescription(ref StringBuilder builder)
        {
            builder.AppendLine("...");
            builder.AppendHorizontalLine(60);
            builder.AppendLine(Colors.Success(Description.ToUpper(CultureInfo.InvariantCulture)));            
        }
    }
}