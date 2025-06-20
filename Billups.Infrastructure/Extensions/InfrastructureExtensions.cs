using Billups.Application.Interfaces;
using Billups.Domain.Interfaces;
using Billups.Infrastructure.ApiClients;
using Billups.Infrastructure.Repositories;
using Billups.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Billups.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var logger = serviceCollection.BuildServiceProvider().GetRequiredService<ILoggerFactory>()
            .CreateLogger(nameof(HttpPolicies));
        serviceCollection.AddHttpClient<IRandomNumberService, RandomNumberApiClient>(c =>
                c.BaseAddress = new Uri(configuration.GetSection("RandomNumberGeneratorApi:BaseUrl").Value!))
            .AddPolicyHandler(HttpPolicies.GetRetryPolicy(logger))
            .AddPolicyHandler(HttpPolicies.GetCircuitBreakerPolicy());

        serviceCollection.Decorate<IRandomNumberService, RandomNumberFallbackService>();

        serviceCollection.AddSingleton<IGameHistoryRepository, InMemoryGameHistoryRepository>();
    }
}