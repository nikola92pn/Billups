using Billups.Application.Dtos;

namespace Billups.Application.Interfaces;

/// <summary>
/// Defines the core gameplay logic used to execute a match between the player and the CPU.
/// </summary>
public interface IGameService
{
    /// <summary>
    /// Executes a game round between the player and the CPU based on the player's selected move.
    /// </summary>
    /// <param name="playerChoiceId">The identifier of the move selected by the player.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>
    /// A task that represents the operation. The task result contains the outcome of the game round as a <see cref="GameResultDto"/>.
    /// </returns>
    public Task<GameResultDto> PlayAgainstCpuAsync(int playerChoiceId, CancellationToken cancellationToken);
}