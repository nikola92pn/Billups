using Billups.Application.Interfaces;
using Billups.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Billups.Application.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddScoped<IChoiceService, ChoiceService>()
            .AddScoped<IGameService, GameService>()
            .AddScoped<IGameHistoryService, GameHistoryService>();
}