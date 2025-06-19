using Billups.Domain.Interfaces;
using Billups.Domain.Models;

namespace Billups.Domain.Rules;

internal class MoveRulesProvider : IMoveRulesProvider
{
    public Dictionary<Move, HashSet<Move>> GetRules(GameMode mode) =>
        mode switch
        {
            GameMode.Rps => new()
            {
                { Move.Rock, [Move.Scissors] },
                { Move.Paper, [Move.Rock] },
                { Move.Scissors, [Move.Paper] }
            },
            GameMode.Rpsls => new()
            {
                { Move.Rock, [Move.Scissors, Move.Lizard] },
                { Move.Paper, [Move.Rock, Move.Spock] },
                { Move.Scissors, [Move.Paper, Move.Lizard] },
                { Move.Lizard, [Move.Spock, Move.Paper] },
                { Move.Spock, [Move.Scissors, Move.Rock] }
            },
            _ => throw new ArgumentOutOfRangeException(nameof(mode))
        };
}