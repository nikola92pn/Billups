using System.Collections.Concurrent;
using System.Collections.Immutable;
using Billups.Domain.Interfaces;
using Billups.Domain.Models;

namespace Billups.Infrastructure.Repositories;

public class InMemoryGameHistoryRepository : IGameHistoryRepository
{
    private static readonly ConcurrentQueue<GameHistory> HistoryQueue = [];
    private const int MaxNumberOfRecords = 10;

    public Task<ImmutableList<GameHistory>> GetRecentHistoryAsync(CancellationToken cancellationToken)
    {
        var recentHistory = HistoryQueue
            .Reverse()
            .Take(MaxNumberOfRecords)
            .ToImmutableList();

        return Task.FromResult(recentHistory);
    }

    public Task SaveAsync(GameHistory history, CancellationToken cancellationToken)
    {
        HistoryQueue.Enqueue(history);
        
        if(HistoryQueue.Count > MaxNumberOfRecords) // maintain number of records in memory
            HistoryQueue.TryDequeue(out _);
        
        return Task.CompletedTask;
    }

    public Task RemoveAll(CancellationToken cancellationToken)
    {
        HistoryQueue.Clear();
        return Task.CompletedTask;
    }
}