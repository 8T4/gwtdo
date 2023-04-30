using System.Linq;
using Gwtdo.Attributes;
using Gwtdo.Linguistic;

namespace Gwtdo.Scenarios;

/// <summary>
/// Use this class to Map Describe Expression To Actions
/// </summary>
/// <typeparam name="TContext"></typeparam>
public abstract class ScenarioFixture<TContext> where TContext : class
{
    protected TContext? Context { get; set; }
    private Scenario<TContext>? Scenario { get; set; }
    
    protected ScenarioVariables Let => Scenario?.Let ?? new ScenarioVariables();

    public void SetScenario(Scenario<TContext> scenario)
    {
        Scenario = scenario;
        Context = scenario.Context;   
    }
    
    public void MapScenarioMethods()
    {
        if (Scenario is null)
            throw new FeatureException("Scenario is not defined");
        
        var methods = 
            from method in GetType().GetMethods()
            where method.GetCustomAttributes(typeof(IGwtCustomAttribute), false).Any()
            select new
            {
                attributes = method.GetCustomAttributes(typeof(IGwtCustomAttribute), false),
                action = method
            };

        if (!methods.Any()) return;

        foreach (var method in methods)
        {
            foreach (var attribute in method.attributes)
            {
                var syntagma = new Syntagma<TContext>(
                    signifier: ((IGwtCustomAttribute)attribute).Description, 
                    signified: _ => method.action.Invoke(this, new object[] { })); 
                
                Scenario.MappedParadigms.AddSyntagma(syntagma);                
            }
        }
    }    
}