using System.Collections.Concurrent;
using System.Collections.Immutable;
using Billups.Domain.Interfaces;
using Billups.Domain.Models;

namespace Billups.Infrastructure.Repositories;

public class InMemoryGameHistoryRepository : IGameHistoryRepository
{
    private readonly ConcurrentQueue<GameHistory> _historyQueue = [];
    private const int MaxNumberOfRecords = 10;

    public Task<ImmutableList<GameHistory>> GetRecentHistoryAsync(CancellationToken cancellationToken)
    {
        var recentHistory = _historyQueue
            .Reverse()
            .Take(MaxNumberOfRecords)
            .ToImmutableList();

        return Task.FromResult(recentHistory);
    }

    public Task SaveAsync(GameHistory history, CancellationToken cancellationToken)
    {
        _historyQueue.Enqueue(history);
        
        if(_historyQueue.Count > MaxNumberOfRecords) // maintain number of records in memory
            _historyQueue.TryDequeue(out _);
        
        return Task.CompletedTask;
    }

    public Task RemoveAll(CancellationToken cancellationToken)
    {
        _historyQueue.Clear();
        return Task.CompletedTask;
    }
}