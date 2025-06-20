using Billups.Application.Dtos;
using Billups.Application.Interfaces;
using Billups.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Billups.Application.Services;

public class GameService(
    IRandomChoiceGenerator randomChoiceGenerator,
    IGameRulesService gameRulesService,
    IGameHistoryService gameHistoryService,
    IChoiceProvider choiceProvider,
    ILogger<GameService> logger) : IGameService
{
    public async Task<GameResultDto> PlayAgainstCpuAsync(int playerChoiceId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting a new game. Player selected choice ID: {PlayerChoiceId}", playerChoiceId);

        var playerChoice = choiceProvider.GetChoice(playerChoiceId);
        var cpuChoice = await randomChoiceGenerator.GetAsync(cancellationToken);
        var gameResult = gameRulesService.Beat(playerChoice.Move, cpuChoice.Move);

        await gameHistoryService.SaveAsync(new(gameResult, playerChoice, cpuChoice, DateTime.UtcNow),
            cancellationToken);

        logger.LogInformation(
            "Game result determined: PlayerMove={PlayerMove}, CpuMove={CpuMove}, Outcome={Result}",
            playerChoice.Move,
            cpuChoice.Move,
            gameResult);

        return new GameResultDto(gameResult, playerChoice, cpuChoice);
    }
}