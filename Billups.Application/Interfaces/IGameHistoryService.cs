using Billups.Application.Dtos;

namespace Billups.Application.Interfaces;

public interface IGameHistoryService
{
    public Task<IEnumerable<GameHistoryDto>> GetRecentHistoryAsync(CancellationToken cancellationToken);
    Task SaveAsync(GameHistoryDto history, CancellationToken cancellationToken);
    Task ResetHistory(CancellationToken cancellationToken);
}