using Billups.Domain.Models;

namespace Billups.Domain.Interfaces;

/// <summary>
/// Provides the current game mode based on the application's context.
/// </summary>
public interface IGameModeProvider
{
    /// <summary>
    /// Gets the current <see cref="GameMode"/> that should be used for game logic execution.
    /// </summary>
    /// <returns>The current <see cref="GameMode"/> value.</returns>
    GameMode GetCurrent();
}