using System.Collections.Immutable;
using Billups.Domain.Models;

namespace Billups.Domain.Rules;

public static class GameModeMoves
{
    public static readonly ImmutableDictionary<GameMode, ImmutableList<Move>> MovesByMode =
        new Dictionary<GameMode, ImmutableList<Move>>
        {
            { GameMode.Rps, new List<Move> { Move.Rock, Move.Paper, Move.Scissors }.ToImmutableList() },
            { GameMode.Rpsls, Enum.GetValues<Move>().ToImmutableList() }
        }.ToImmutableDictionary();
}