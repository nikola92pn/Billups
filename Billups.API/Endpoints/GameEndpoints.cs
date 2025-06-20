using System.Net;
using Billups.Api.Mappers;
using Billups.Api.Models;
using Billups.Application.Interfaces;
using FluentValidation;

namespace Billups.Api.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this WebApplication app)
    {
        var gamePlayGroup = app.MapGroup("")
            .WithTags("Game");

        gamePlayGroup.MapPost("/play", PlayAsync)
            .WithName("Play Against CPU")
            .Accepts<PlayRequest>("application/json")
            .Produces<PlayResponse>()
            .Produces<ValidationErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);

        gamePlayGroup.MapGet("/history", GetRecentHistoryAsync)
            .WithName("Get recent scores")
            .Produces<HistoryResponse[]>()
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);

        gamePlayGroup.MapPost("/history-clear", ResetHistoryAsync)
            .WithName("Reset recent scores")
            .Produces((int)HttpStatusCode.NoContent)
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }

    private static async Task<IResult> PlayAsync(PlayRequest request, IValidator<PlayRequest> validator,
        IGameService gameService,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.ToResponse());

        var gameResult = await gameService.PlayAgainstCpuAsync(request.Player, cancellationToken);
        return Results.Ok(gameResult.ToResponse());
    }

    private static async Task<IResult> GetRecentHistoryAsync(IGameHistoryService gameHistoryService,
        CancellationToken cancellationToken)
    {
        var history = await gameHistoryService.GetRecentHistoryAsync(cancellationToken);
        return Results.Ok(history.Select(h => h.ToResponse()));
    }

    private static async Task<IResult> ResetHistoryAsync(IGameHistoryService gameHistoryService,
        CancellationToken cancellationToken)
    {
        await gameHistoryService.ResetHistory(cancellationToken);
        return Results.NoContent();
    }
}