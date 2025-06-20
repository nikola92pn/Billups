using Billups.Api.Mappers;
using Billups.Api.Models;
using Billups.Application.Interfaces;

namespace Billups.Api.Endpoints;

public static class ChoicesEndpoints
{
    public static void MapChoicesEndpoints(this WebApplication app)
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
    }

    private static IResult GetChoices(IChoiceProvider choiceProvider, CancellationToken cancellationToken)
    {
        var choices = choiceProvider.GetAll();
        return Results.Ok(choices.Select(c => c.ToResponse()));
    }

    private static async Task<IResult> GetRandomChoiceAsync(IRandomChoiceGenerator randomChoiceGenerator,
        CancellationToken cancellationToken)
    {
        var choice = await randomChoiceGenerator.GetAsync(cancellationToken);
        return Results.Ok(choice.ToResponse());
    }
}