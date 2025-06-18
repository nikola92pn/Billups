using Billups.Application.Interfaces;

namespace Billups.Infrastructure.ApiClients;

internal class RandomNumberApiClient(HttpClient httpClient) : IRandomNumberService
{
    public Task<int> GetRandomNumber(CancellationToken cancellationToken)
    {
        var response = httpClient.GetAsync("/", )
    }
}