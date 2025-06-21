using Billups.Application.Dtos;

namespace Billups.Application.Interfaces;

/// <summary>
/// Defines the application-level operations for accessing and managing game history data.
/// </summary>
public interface IGameHistoryService
{
    /// <summary>
    /// Retrieves the most recent game history records.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>
    /// A task that represents the operation. The task result contains a collection of <see cref="GameHistoryDto"/>.
    /// </returns>
    Task<IEnumerable<GameHistoryDto>> GetRecentHistoryAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Saves a new game history record.
    /// </summary>
    /// <param name="history">The game history data transfer object to be stored.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that represents the save operation.</returns>
    Task SaveAsync(GameHistoryDto history, CancellationToken cancellationToken);

    /// <summary>
    /// Clears all stored game history records.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that represents the clear operation.</returns>
    Task ResetHistory(CancellationToken cancellationToken);
}