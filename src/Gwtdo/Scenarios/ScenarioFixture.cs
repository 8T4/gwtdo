using System.Linq;
using Gwtdo.Attributes;
using Gwtdo.Linguistic;

namespace Gwtdo.Scenarios;

/// <summary>
/// Use this class to Map Scenario Expression To Actions
/// </summary>
/// <typeparam name="TContext"></typeparam>
public abstract class ScenarioFixture<TContext> where TContext : IFeatureContext
{
    protected TContext Context { get; set; }
    private Scenario<TContext> SCENARIO { get; set; }
    
    protected ScenarioVariables Let => SCENARIO?.Let;

    public void SetScenario(Scenario<TContext> scenario)
    {
        SCENARIO = scenario;
        Context = scenario.Context;   
    }
    
    public void MapScenarioMethods()
    {
        var methods = 
            from method in this.GetType().GetMethods()
            where method.GetCustomAttributes(typeof(IGwtCustomAttribute), false).Any()
            select new
            {
                attribute = (IGwtCustomAttribute)method.GetCustomAttributes(typeof(IGwtCustomAttribute), false).FirstOrDefault(),
                action = method
            };

        if (!methods.Any()) return;

        foreach (var method in methods)
        {
            var syntagma = new Syntagma<TContext>(method.attribute.Description, _ => method.action.Invoke(this, new object[] { }));
            SCENARIO.MappedParadigms.AddSyntagma(syntagma);
        }
    }    
}