using System.Net;
using Billups.Api.Mappers;
using Billups.Api.Models;
using Billups.Application.Interfaces;

namespace Billups.Api.Endpoints;

/// <summary>
/// Defines HTTP endpoints for retrieving and clearing game history.
/// </summary>
public static class HistoryEndpoints
{
    /// <summary>
    /// Maps endpoints related to game history operations such as fetching recent history or clearing it.
    /// </summary>
    /// <param name="app">The web application to configure.</param>
    public static void MapHistoryEndpoints(this WebApplication app)
    {
        var gamePlayGroup = app.MapGroup("")
            .WithTags("History");

        gamePlayGroup.MapGet("/history", GetRecentHistoryAsync)
            .WithName("GetRecentScores")
            .WithDescription("Get recent scores")
            .Produces<HistoryResponse[]>()
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);

        gamePlayGroup.MapDelete("/history", ResetHistoryAsync)
            .WithName("ResetRecentScores")
            .WithDescription("Reset recent scores")
            .Produces((int)HttpStatusCode.NoContent)
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }
    
    /// <summary>
    /// Retrieves the most recent game history records.
    /// </summary>
    /// <param name="gameHistoryService">Service that provides access to game history data.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>An HTTP result containing the list of recent game history records.</returns>
    private static async Task<IResult> GetRecentHistoryAsync(IGameHistoryService gameHistoryService,
        CancellationToken cancellationToken)
    {
        var history = await gameHistoryService.GetRecentHistoryAsync(cancellationToken);
        return Results.Ok(history.Select(h => h.ToResponse()));
    }

    /// <summary>
    /// Clears all in-memory game history.
    /// </summary>
    /// <param name="gameHistoryService">Service responsible for managing game history.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>An HTTP result indicating success with no content.</returns>
    private static async Task<IResult> ResetHistoryAsync(IGameHistoryService gameHistoryService,
        CancellationToken cancellationToken)
    {
        await gameHistoryService.ResetHistory(cancellationToken);
        return Results.NoContent();
    }
}