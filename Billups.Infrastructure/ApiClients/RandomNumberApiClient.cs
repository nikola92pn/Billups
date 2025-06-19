using System.Net.Http.Json;
using Billups.Application.Interfaces;
using Billups.Infrastructure.ApiClients.Models;
using Microsoft.Extensions.Logging;

namespace Billups.Infrastructure.ApiClients;

internal class RandomNumberApiClient(HttpClient httpClient, ILogger<RandomNumberApiClient> logger) : IRandomNumberService
{
    public async Task<int> GetRandomNumberAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting random number from external api");

        using var response = await httpClient.GetAsync("", cancellationToken);
        response.EnsureSuccessStatusCode();

        var randomNumberResponse = await response.Content.ReadFromJsonAsync<RandomNumberResponse>(cancellationToken);

        if (randomNumberResponse is null)
            throw new NullReferenceException("Random number response is null");

        logger.LogInformation("Random number returned: {RandomNumber}", randomNumberResponse.RandomNumber);
        return randomNumberResponse.RandomNumber;
    }
}