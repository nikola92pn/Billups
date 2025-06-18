using Billups.Application.Interfaces;

namespace Billups.Api.Endpoints;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/v1/test")
            .WithTags("test");

        group.MapGet("/", GetTest);
    }

    private static async Task<IResult> GetTest(IGameService gameService, CancellationToken cancellationToken)
    {
        await gameService.Play(cancellationToken);
        return Results.NoContent();
    }
}