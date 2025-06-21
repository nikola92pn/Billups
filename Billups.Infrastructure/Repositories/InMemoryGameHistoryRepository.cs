using System.Collections.Concurrent;
using System.Collections.Immutable;
using Billups.Domain.Interfaces;
using Billups.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Billups.Infrastructure.Repositories;

public class InMemoryGameHistoryRepository(ILogger<InMemoryGameHistoryRepository> logger) : IGameHistoryRepository
{
    private readonly ConcurrentQueue<GameHistory> _historyQueue = [];
    private const int MaxNumberOfRecords = 10;

    public Task<ImmutableList<GameHistory>> GetRecentHistoryAsync(CancellationToken cancellationToken)
    {
        var recentHistory = _historyQueue
            .Reverse()
            .Take(MaxNumberOfRecords)
            .ToImmutableList();

        logger.LogInformation("Retrieved {Count} recent game history entries.", recentHistory.Count);
        return Task.FromResult(recentHistory);
    }

    public Task SaveAsync(GameHistory history, CancellationToken cancellationToken)
    {
        _historyQueue.Enqueue(history);
        logger.LogInformation(
            "Saved game result: PlayerMove={PlayerMove}, CpuMove={CpuMove}, Result={Result}, Time={CreatedAt}",
            history.PlayerMove,
            history.CpuMove,
            history.Result,
            history.CreatedAt
        );
        
        if(_historyQueue.Count > MaxNumberOfRecords) // maintain number of records in memory
            _historyQueue.TryDequeue(out _);
        
        return Task.CompletedTask;
    }

    public Task RemoveAllAsync(CancellationToken cancellationToken)
    {
        _historyQueue.Clear();
        logger.LogWarning("Game history cleared.");
        return Task.CompletedTask;
    }
}