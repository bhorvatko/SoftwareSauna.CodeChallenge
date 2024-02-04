using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Serilog;
using Xunit.Abstractions;

namespace SoftwareSauna.CodeChallenge.IntegrationTests;

internal class ApiFactory
    : WebApplicationFactory<Host.Program>
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ApiFactory(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseSerilog((ctx, config) =>
        {
            config.WriteTo.TestOutput(_testOutputHelper);
        });

        return base.CreateHost(builder);
    }
}
