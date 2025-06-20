using Billups.Application.Dtos;
using Billups.Application.Interfaces;
using Billups.Application.Mappers;
using Billups.Domain.Interfaces;

namespace Billups.Application.Services;

public class GameHistoryService(IGameHistoryRepository gameHistoryRepository) : IGameHistoryService
{
    public async Task<IEnumerable<GameHistoryDto>> GetRecentHistoryAsync(CancellationToken cancellationToken)
    {
        var historyList = await gameHistoryRepository.GetRecentHistoryAsync(cancellationToken);
        return historyList.Select(h => h.ToGameHistoryDto());
    }

    public async Task SaveAsync(GameHistoryDto historyDto, CancellationToken cancellationToken)
    {
        var history = historyDto.ToGameHistory();
        await gameHistoryRepository.SaveAsync(history, cancellationToken);
    }
}