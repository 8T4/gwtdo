using System;
using Gwtdo.Constants;
using Gwtdo.Linguistic;

namespace Gwtdo.Steps;

[Obsolete]
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
        var syntagma = new Syntagma<T>(GwtConstants.GIVEN, null);

        if (describe.Feature.SCENARIO.Paradigms.SyntagmaExists(syntagma)) return describe.Feature;
        
        describe.Feature.SCENARIO.Paradigms.AddSyntagma(syntagma);
        describe.Feature.SCENARIO.MappedParadigms.AddSyntagma(syntagma);
        return describe.Feature;
    }    
}