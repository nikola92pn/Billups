using Billups.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Billups.Infrastructure.Services;

public class RandomNumberFallbackService(IRandomNumberService inner, ILogger<RandomNumberFallbackService> logger) : IRandomNumberService
{
    public async Task<int> GetRandomNumberAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await inner.GetRandomNumberAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Random number external service failed — using fallback");
            return Random.Shared.Next(1, 100);
        }
    }
}