using Microsoft.Extensions.DependencyInjection;

namespace SoftwareSauna.CodeChallenge.Infrastructure.Mediator;

internal static class Setup
{
    public static IServiceCollection AddMediator(this IServiceCollection services) =>
        services
            .AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
}
