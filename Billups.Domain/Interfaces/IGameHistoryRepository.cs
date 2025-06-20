using System.Collections.Immutable;
using Billups.Domain.Models;

namespace Billups.Domain.Interfaces;

public interface IGameHistoryRepository
{
    public Task<ImmutableList<GameHistory>> GetRecentHistoryAsync(CancellationToken cancellationToken);
    Task SaveAsync(GameHistory history, CancellationToken cancellationToken);
}