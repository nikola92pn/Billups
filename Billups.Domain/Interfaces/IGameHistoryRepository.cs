using System.Collections.Immutable;
using Billups.Domain.Models;

namespace Billups.Domain.Interfaces;

/// <summary>
/// Defines the contract for managing the game's history records,
/// such as retrieving recent games, saving new results, and clearing stored data.
/// </summary>
public interface IGameHistoryRepository
{
    /// <summary>
    /// Retrieves the most recent game history records.
    /// </summary>
    /// /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of recent game history entries.</returns>
    Task<ImmutableList<GameHistory>> GetRecentHistoryAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Saves a new game history entry.
    /// </summary>
    /// <param name="history">The game history record to save.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task that represents the save operation.</returns>
    Task SaveAsync(GameHistory history, CancellationToken cancellationToken);

    /// <summary>
    /// Removes all stored game history records.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A task that represents the clear operation.</returns>
    Task RemoveAllAsync(CancellationToken cancellationToken);
}
