using Billups.Api.Mappers;
using Billups.Api.Models;
using Billups.Application.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace Billups.Api.Endpoints;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var choiceGroup = app.MapGroup("")
            .WithTags("Choice");

        choiceGroup.MapGet("/choices", GetChoices)
            .WithName("Get All Choices")
            .Produces<ChoiceResponse[]>()
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
        
        choiceGroup.MapGet("/choice", GetRandomChoiceAsync)
            .WithName("Get Random Choice")
            .Produces<ChoiceResponse>()
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);

        var gamePlayGroup = app.MapGroup("")
            .WithTags("Play");

        gamePlayGroup.MapPost("/play", PlayAsync)
            .WithName("Play Against CPU")
            .Accepts<PlayRequest>("application/json")
            .Produces<GameResultResponse>()
            .Produces<ValidationErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }

    private static IResult GetChoices(IChoiceService choiceService, CancellationToken cancellationToken)
    {
        var choices = choiceService.GetAll();
        return Results.Ok(choices.Select(c => c.ToResponse()));
    }

    private static async Task<IResult> GetRandomChoiceAsync(IChoiceService choiceService,
        CancellationToken cancellationToken)
    {
        var choice = await choiceService.GetRandomChoiceAsync(cancellationToken);
        return Results.Ok(choice.ToResponse());
    }

    private static async Task<IResult> PlayAsync(PlayRequest request, IValidator<PlayRequest> validator, IGameService gameService,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.ToResponse());
           
        
        var gameResult = await gameService.PlayAgainstCpuAsync(request.Player, cancellationToken);
        return Results.Ok(gameResult.ToResponse());
    }
}