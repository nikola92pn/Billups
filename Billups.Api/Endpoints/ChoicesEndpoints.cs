using Billups.Api.Mappers;
using Billups.Api.Models;
using Billups.Application.Interfaces;

namespace Billups.Api.Endpoints;

/// <summary>
/// Maps HTTP endpoints related to player choices.
/// </summary>
public static class ChoicesEndpoints
{
    /// <summary>
    /// Registers the choice-related endpoints.
    /// </summary>
    /// <param name="app">The WebApplication instance to map endpoints to.</param>
    public static void MapChoicesEndpoints(this WebApplication app)
    {
        var choiceGroup = app.MapGroup("")
            .WithTags("Choice");

        choiceGroup.MapGet("/choices", GetChoices)
            .WithName("GetAllChoices")
            .WithDescription("Get All Choices")
            .Produces<ChoiceResponse[]>()
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
        
        choiceGroup.MapGet("/choice", GetRandomChoiceAsync)
            .WithName("GetRandomChoice")
            .WithName("Get Random Choice")
            .Produces<ChoiceResponse>()
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// Returns all available player choices for the current game mode.
    /// </summary>
    /// <param name="currentChoiceResolver">Service that resolves choices based on current game mode.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A list of choices with Id and move information.</returns>
    private static IResult GetChoices(ICurrentChoiceResolver currentChoiceResolver, CancellationToken cancellationToken)
    {
        var choices = currentChoiceResolver.GetAll();
        return Results.Ok(choices.Select(c => c.ToResponse()));
    }
    
    /// <summary>
    /// Returns a randomly selected choice for the CPU based on the current game mode.
    /// </summary>
    /// <param name="randomChoiceGenerator">Service that provides a random choice.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A randomly selected choice.</returns>
    private static async Task<IResult> GetRandomChoiceAsync(IRandomChoiceGenerator randomChoiceGenerator,
        CancellationToken cancellationToken)
    {
        var choice = await randomChoiceGenerator.GetAsync(cancellationToken);
        return Results.Ok(choice.ToResponse());
    }
}