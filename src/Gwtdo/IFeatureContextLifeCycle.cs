using System.Threading.Tasks;

namespace Gwtdo;

public interface IFeatureContextLifeCycle
{
    public void Setup();
    public void TearDown();
}

public interface IFeatureContextAsyncLifeCycle
{
    public Task SetupAsync();
    public Task TearDownAsync();
}