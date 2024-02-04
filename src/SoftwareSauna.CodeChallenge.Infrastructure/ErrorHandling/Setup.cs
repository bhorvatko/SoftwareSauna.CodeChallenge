using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SoftwareSauna.CodeChallenge.Infrastructure.ErrorHandling;

internal static class Setup
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services) =>
        services
            .AddExceptionHandler<DomainArgumentExceptionHandler>()
            .AddExceptionHandler<DomainInvalidOperationExceptionHandler>()
            .AddProblemDetails();

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app) =>
        app.UseExceptionHandler();
}
