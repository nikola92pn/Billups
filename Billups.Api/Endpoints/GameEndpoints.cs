using Billups.Api.Mappers;
using Billups.Api.Models;
using Billups.Application.Interfaces;
using FluentValidation;

namespace Billups.Api.Endpoints;

/// <summary>
/// Defines HTTP endpoints related to game play.
/// </summary>
public static class GameEndpoints
{
    /// /// <summary>
    /// Registers game-related endpoints such as playing against the CPU.
    /// </summary>
    /// <param name="app">The web application to configure.</param>
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
    }

    /// <summary>
    /// Handles a game round where the player plays against the CPU.
    /// Validates the input and returns the result of the game.
    /// </summary>
    /// <param name="request">The player’s choice in the game.</param>
    /// <param name="validator">Validator for the input request.</param>
    /// <param name="gameService">Service that executes the game logic.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The result of the game round.</returns>
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
}