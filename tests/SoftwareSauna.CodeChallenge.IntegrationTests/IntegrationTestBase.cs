using Xunit.Abstractions;

namespace SoftwareSauna.CodeChallenge.IntegrationTests;

public abstract class IntegrationTestBase
{
    protected HttpClient _client;

    public IntegrationTestBase(ITestOutputHelper testOutputHelper)
    {
        ApiFactory factory = new ApiFactory(testOutputHelper);

        _client = factory.CreateClient();
    }
}
