using Billups.Application.Dtos;

namespace Billups.Application.Interfaces;

public interface IGameService
{
    public Task<GameResultDto> PlayAgainstCpuAsync(int playerChoiceId, CancellationToken cancellationToken);
}