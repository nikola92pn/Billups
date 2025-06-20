using Billups.Application.Constants;
using Billups.Application.Dtos;
using Billups.Application.Interfaces;
using Billups.Domain.Interfaces;

namespace Billups.Application.Services;

public class GameService(IChoiceService choiceService, IGameRulesService gameRulesService, IGameHistoryService gameHistoryService): IGameService
{
    public async Task<GameResultDto> PlayAgainstCpuAsync(int playerChoiceId, CancellationToken cancellationToken)
    {
        var playerChoice = Choices.GetChoice(playerChoiceId);
        var cpuChoice = await choiceService.GetRandomChoiceAsync(cancellationToken);
        var gameResult = gameRulesService.Beat(playerChoice.Move, cpuChoice.Move);
        
        await gameHistoryService.SaveAsync(new(gameResult, playerChoice, cpuChoice, DateTime.UtcNow), cancellationToken);
        
        return new GameResultDto(gameResult, playerChoice, cpuChoice);
    }
}