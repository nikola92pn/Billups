using System.Net;
using System.Net.Http.Json;
using Billups.Api.Models;
using Billups.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Billups.IntegrationTests;

public class GamePlayEndpointsTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Play_ReturnsValidResult()
    {
        // Arrange
        var playRequest = new PlayRequest(1);

        // Act
        var response = await _client.PostAsJsonAsync("/play", playRequest);

        // Assert
        response.EnsureSuccessStatusCode();
        var playResponse = await response.Content.ReadFromJsonAsync<PlayResponse>();
        Assert.NotNull(playResponse);
        Assert.Equal(1, playResponse.Player);
        Assert.True(Enum.TryParse<GameResult>(playResponse.Results, out _));
    }

    [Fact]
    public async Task PlayEndpoint_Returns400_WhenInputIsInvalid()
    {
        // Arrange
        var playRequest = new PlayRequest(-1); // invalid input

        // Act
        var response = await _client.PostAsJsonAsync("/play", playRequest);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var validationError = await response.Content.ReadFromJsonAsync<ValidationErrorResponse>();
        Assert.NotNull(validationError);
        Assert.Contains("Player", string.Join(',', validationError.Errors.Select(e => e.Property)));
    }
    
    [Fact]
    public async Task FullGamePlay_WorksEndToEnd()
    {
        // 1. Get available choices
        var choicesResponse = await _client.GetAsync("/choices");
        choicesResponse.EnsureSuccessStatusCode();
        var choices = await choicesResponse.Content.ReadFromJsonAsync<IList<ChoiceResponse>>();
        Assert.NotNull(choices);
        Assert.NotEmpty(choices);
        var playerChoice = choices.First();

        // 2. Play a round using the first available choice
        var playRequest = new PlayRequest(playerChoice.Id);
        var playResponse = await _client.PostAsJsonAsync("/play", playRequest);
        playResponse.EnsureSuccessStatusCode();
        var playResult = await playResponse.Content.ReadFromJsonAsync<PlayResponse>();
        Assert.NotNull(playResult);
        Assert.Equal(playerChoice.Id, playResult.Player);

        // 3. Get history, should contain at least one record for the played round
        var historyResponse = await _client.GetAsync("/history");
        historyResponse.EnsureSuccessStatusCode();
        var histories = await historyResponse.Content.ReadFromJsonAsync<IEnumerable<HistoryResponse>>();
        Assert.NotNull(histories);
        Assert.Contains(histories, h => h.PlayerChoice.Id == playerChoice.Id);

        // 4. Reset the history
        var resetResponse = await _client.DeleteAsync("/history");
        Assert.Equal(HttpStatusCode.NoContent, resetResponse.StatusCode);

        // 5. Get history again, should be empty
        var historyAfterResetResponse = await _client.GetAsync("/history");
        historyAfterResetResponse.EnsureSuccessStatusCode();
        var historiesAfterReset = await historyAfterResetResponse.Content.ReadFromJsonAsync<IEnumerable<HistoryResponse>>();
        Assert.NotNull(historiesAfterReset);
        Assert.Empty(historiesAfterReset);
    }
}