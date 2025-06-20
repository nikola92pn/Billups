using Billups.Application.Dtos;
using Billups.Application.Interfaces;
using Billups.Application.Mappers;
using Billups.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Billups.Application.Services;

public class GameHistoryService(IGameHistoryRepository gameHistoryRepository, ILogger<GameHistoryService> logger) : IGameHistoryService
{
    public async Task<IEnumerable<GameHistoryDto>> GetRecentHistoryAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching recent game history...");
        
        var historyList = await gameHistoryRepository.GetRecentHistoryAsync(cancellationToken);
        return historyList.Select(h => h.ToGameHistoryDto());
    }

    public async Task SaveAsync(GameHistoryDto historyDto, CancellationToken cancellationToken)
    {
        logger.LogInformation("Saving game history: Player={Player}, CPU={Cpu}, Result={Result}, CreatedAt={CreatedAt}",
            historyDto.PlayerChoice.Move,
            historyDto.CpuChoice.Move,
            historyDto.Result,
            historyDto.CreatedAt);
        
        var history = historyDto.ToGameHistory();
        await gameHistoryRepository.SaveAsync(history, cancellationToken);
        
        logger.LogDebug("Game history saved successfully.");
    }
    
    public async Task ResetHistory(CancellationToken cancellationToken)
    {
        await gameHistoryRepository.RemoveAll(cancellationToken);
        logger.LogInformation("Game history cleared.");
    }
}