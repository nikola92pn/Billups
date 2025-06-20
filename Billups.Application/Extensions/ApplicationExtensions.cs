using Billups.Application.Interfaces;
using Billups.Application.Mappers;
using Billups.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Billups.Application.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationServices(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddScoped<IRandomChoiceGenerator, RandomChoiceGenerator>()
            .AddScoped<IGameService, GameService>()
            .AddScoped<IGameHistoryService, GameHistoryService>()
            .AddScoped<IGameHistoryMapper, GameHistoryMapper>()
            .AddSingleton<IChoiceProvider, ChoiceProvider>();
}