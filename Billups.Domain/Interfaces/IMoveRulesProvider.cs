using System.Collections.Immutable;
using Billups.Domain.Models;

namespace Billups.Domain.Interfaces;

/// <summary>
/// Provides the rules that define which moves defeat other moves for a given game mode.
/// </summary>
public interface IMoveRulesProvider
{
    /// <summary>
    /// Retrieves the rule set for the specified <see cref="GameMode"/>, mapping each move to the set of moves it can defeat.
    /// </summary>
    /// <param name="mode">The game mode for which to retrieve the rules.</param>
    /// <returns>
    /// An immutable dictionary where each key is a <see cref="Move"/>, and each value is an immutable set of moves that the key move defeats.
    /// </returns>
    ImmutableDictionary<Move, ImmutableHashSet<Move>> GetRules(GameMode mode);
}