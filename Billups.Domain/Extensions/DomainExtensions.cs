using Billups.Domain.Interfaces;
using Billups.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Billups.Domain.Extensions;

public static class DomainExtensions
{
    public static void AddDomainServices(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddScoped<IGameRulesService, GameRulesService>()
            .AddScoped<IMoveComparerFactory, MoveComparerFactory>()
            .AddSingleton<IMoveRulesProvider, MoveRulesProvider>();
}