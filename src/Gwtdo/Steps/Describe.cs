using Gwtdo.Constants;
using Gwtdo.Linguistic;

namespace Gwtdo.Steps;

public sealed class Describe<T> where T : IFeatureContext
{
    public Feature<T> Value { get; }
    public Describe<T> And => this;

    private Describe(Feature<T> value)
    {
        Value = value;
    }

    internal static Describe<T> Create(Feature<T> value) => new(value);
    
    public static Feature<T> operator |(Describe<T> describe, string other) => describe.Value;    
    
    public static Feature<T> operator |(Describe<T> describe, Arrange<T> other)
    {
        var syntagma = new Syntagma<T>(GwtConstants.GIVEN, null);

        if (describe.Value.SCENARIO.Paradigms.SyntagmaExists(syntagma)) return describe.Value;
        
        describe.Value.SCENARIO.Paradigms.AddSyntagma(syntagma);
        describe.Value.SCENARIO.MappedParadigms.AddSyntagma(syntagma);
        return describe.Value;
    }    
}