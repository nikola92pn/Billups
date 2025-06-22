using Billups.Api.Models;
using Billups.Application.Dtos;

namespace Billups.Api.Mappers;

public static class GameResultMapper
{
    public static PlayResponse ToResponse(this GameResultDto gameResult)
        => new(gameResult.Result.ToString(), gameResult.PlayerChoice.Id, gameResult.CpuChoice.Id);
}