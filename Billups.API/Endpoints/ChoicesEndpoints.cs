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
}