using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SoftwareSauna.CodeChallenge.Infrastructure.ErrorHandling;
using SoftwareSauna.CodeChallenge.Infrastructure.Mediator;
using SoftwareSauna.CodeChallenge.Infrastructure.OpenApi;

namespace SoftwareSauna.CodeChallenge.Infrastructure;

public static class Setup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddControllers();

        services
            .AddMediator()
            .AddOpenApi()
            .AddErrorHandling();

        return services;
    }

    public static WebApplication UseInfrastrcture(this WebApplication app)
    {
        app
            .UseSerilogRequestLogging()
            .UseHttpsRedirection()
            .UseErrorHandling();

        app.MapControllers();

        app.UseOpenApi();

        return app;
    }
}
