using System.Net.Http.Json;
using Billups.Api.Models;
using Billups.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Billups.IntegrationTests;

public class ChoicesEndpointsTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetChoices_ReturnsChoices()
    {
        // Act
        var response = await _client.GetAsync("/choices");

        // Assert
        response.EnsureSuccessStatusCode();
        var choices = await response.Content.ReadFromJsonAsync<IList<ChoiceResponse>>();
        Assert.NotNull(choices);
        Assert.NotEmpty(choices); // Should return at least one choice
        Assert.All(choices, c =>
        {
            Assert.True(c.Id > 0);
            Assert.False(string.IsNullOrEmpty(c.Name));
        });
    }

    [Fact]
    public async Task GetRandomChoice_ReturnsValidChoice()
    {
        // Act
        var response = await _client.GetAsync("/choice");

        // Assert
        response.EnsureSuccessStatusCode();
        var choice = await response.Content.ReadFromJsonAsync<ChoiceResponse>();
        Assert.NotNull(choice);
        Assert.True(choice.Id > 0);
        Assert.False(string.IsNullOrEmpty(choice.Name));
    }
    
    [Fact]
    public async Task GetChoices_ReturnsDifferentChoices_ForDifferentGameModes()
    {
        // Arrange
        var classicRequest = new HttpRequestMessage(HttpMethod.Get, "/choices");
        classicRequest.Headers.Add("x-game-mode", nameof(GameMode.Rps));

        var advancedRequest = new HttpRequestMessage(HttpMethod.Get, "/choices");
        advancedRequest.Headers.Add("X-Game-Mode", nameof(GameMode.Rpsls));

        // Act
        var classicResponse = await _client.SendAsync(classicRequest);
        var advancedResponse = await _client.SendAsync(advancedRequest);

        // Assert
        classicResponse.EnsureSuccessStatusCode();
        advancedResponse.EnsureSuccessStatusCode();

        var classicChoices = await classicResponse.Content.ReadFromJsonAsync<IList<ChoiceResponse>>();
        var advancedChoices = await advancedResponse.Content.ReadFromJsonAsync<IList<ChoiceResponse>>();

        Assert.NotNull(classicChoices);
        Assert.NotNull(advancedChoices);

        // The sets of choices should differ if modes differ
        Assert.NotEqual(classicChoices.Count, advancedChoices.Count);
        Assert.NotEqual(
            classicChoices.Select(c => c.Id).ToList(),
            advancedChoices.Select(c => c.Id).ToList()
        );
    }
}