using Billups.Application.Dtos;
using Billups.Application.Interfaces;
using Billups.Domain.Interfaces;

namespace Billups.Application.Services;

public class GameService(IChoiceService choiceService, IGameRulesService gameRulesService): IGameService
{
    public async Task<GameResultDto> PlayAgainstCpuAsync(int playerChoiceId, CancellationToken cancellationToken)
    {
        var playerChoice = choiceService.GetChoice(playerChoiceId);
        var cpuChoice = await choiceService.GetRandomChoiceAsync(cancellationToken);
        var gameResult = gameRulesService.Beat(playerChoice.Move, cpuChoice.Move);
        return new GameResultDto(gameResult, playerChoice,  cpuChoice);
    }
}