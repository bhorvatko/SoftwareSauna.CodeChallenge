using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SoftwareSauna.CodeChallenge.Infrastructure.OpenApi;

internal static class Setup
{
    public static IServiceCollection AddOpenApi(this IServiceCollection services) =>
        services
            .AddSwaggerGen();

    public static WebApplication UseOpenApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app
            .UseSwagger()
            .UseSwaggerUI();
        }

        return app;
    }
}
