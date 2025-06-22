using Billups.Api.Services;
using Billups.Domain.Interfaces;

namespace Billups.Api.Extensions;

public static class ApiExtensions
{
    public static void AddApiServices(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IGameModeProvider, GameModeProvider>();
}