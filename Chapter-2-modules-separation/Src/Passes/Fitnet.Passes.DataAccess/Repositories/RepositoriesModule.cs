namespace EvolutionaryArchitecture.Fitnet.Passes.DataAccess.Repositories;

using Microsoft.Extensions.DependencyInjection;

internal static class RepositoriesModule
{
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPassesRepository, PassesRepository>();

        return services;
    }
}
