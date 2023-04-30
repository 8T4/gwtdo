namespace Gwtdo;

public interface IFeatureContext { }

public interface IFeatureContextLifeCycle
{
    public void Setup();
    public void TearDown();
}
