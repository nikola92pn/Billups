using Billups.Domain.Models;

namespace Billups.Domain.Interfaces;

/// <summary>
/// Provides game rule evaluation logic to determine the outcome of a move comparison.
/// </summary>
public interface IGameRulesService
{
    /// <summary>
    /// Compares the player's move against the opponent's move and determines the result.
    /// </summary>
    /// <param name="playerMove">The move selected by the player.</param>
    /// <param name="otherMove">The move selected by the opponent.</param>
    /// <returns>
    /// A <see cref="GameResult"/> value indicating whether the player won, lost, or the game was a draw.
    /// </returns>
    GameResult Beat(Move playerMove, Move otherMove);
}