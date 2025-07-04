using Billups.Application.Dtos;
using Billups.Application.Interfaces;
using Billups.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Billups.Application.Services;

public class GameHistoryService(IGameHistoryRepository gameHistoryRepository, IGameHistoryMapper gameHistoryMapper, IGameModeProvider gameModeProvider, ILogger<GameHistoryService> logger) : IGameHistoryService
{
    public async Task<IEnumerable<GameHistoryDto>> GetRecentHistoryAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching recent game history...");
        
        var historyList = await gameHistoryRepository.GetRecentHistoryAsync(gameModeProvider.GetCurrent(), cancellationToken);
        return historyList.Select(gameHistoryMapper.ToDto);
    }

    public async Task SaveAsync(GameHistoryDto historyDto, CancellationToken cancellationToken)
    {
        logger.LogInformation("Saving game history: Player={Player}, CPU={Cpu}, Result={Result}, CreatedAt={CreatedAt}",
            historyDto.PlayerChoice.Move,
            historyDto.CpuChoice.Move,
            historyDto.Result,
            historyDto.CreatedAt);
        
        var history = gameHistoryMapper.ToDomain(historyDto);
        await gameHistoryRepository.SaveAsync(history, cancellationToken);
        
        logger.LogDebug("Game history saved successfully.");
    }
    
    public async Task ResetHistory(CancellationToken cancellationToken)
    {
        await gameHistoryRepository.RemoveAllAsync(cancellationToken);
        logger.LogInformation("Game history cleared.");
    }
}