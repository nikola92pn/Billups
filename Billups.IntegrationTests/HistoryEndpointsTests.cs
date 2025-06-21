using System.Net;
using System.Net.Http.Json;
using Billups.Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Billups.IntegrationTests;

public class HistoryEndpointsTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetRecentHistory_ReturnsHistoryResponses()
    {
        // Act
        var response = await _client.GetAsync("/history");

        // Assert
        response.EnsureSuccessStatusCode();
        var histories = await response.Content.ReadFromJsonAsync<IEnumerable<HistoryResponse>>();
        Assert.NotNull(histories);
        Assert.All(histories, h => Assert.True(h.PlayerChoice.Id > 0));
    }

    [Fact]
    public async Task ResetHistory_ReturnsNoContent()
    {
        // Act
        var response = await _client.DeleteAsync("/history");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}