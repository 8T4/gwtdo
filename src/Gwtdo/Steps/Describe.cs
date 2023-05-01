using System;
using Gwtdo.Output;
using Gwtdo.Scenarios.Linguistic;

namespace Gwtdo.Steps;

[Obsolete("Consider don't use this class in your projects")]
public sealed class Describe<T> : Step<T> where T : class
{
    public Describe<T> And => this;

    private Describe(Feature<T> value): base(value)
    {
    }

    public static Describe<T> Create(Feature<T> value) => new(value);
    
    public static Feature<T> operator |(Describe<T> describe, string other) => describe.Feature;    
    public static Feature<T> operator |(Describe<T> describe, Arrange<T> other)
    {
        var syntagma = new Syntagma<T>(OutputConstants.GIVEN, null);

        if (describe.Feature.Scenario.Paradigms.SyntagmaExists(syntagma)) return describe.Feature;
        
        describe.Feature.Scenario.Paradigms.AddSyntagma(syntagma);
        describe.Feature.Scenario.MappedParadigms.AddSyntagma(syntagma);
        return describe.Feature;
    }    
}