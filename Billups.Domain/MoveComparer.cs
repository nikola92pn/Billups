using Billups.Domain.Models;
using static Billups.Domain.Models.Move;

namespace Billups.Domain;

internal class MoveComparer : IComparer<Move>
{
    private static readonly Dictionary<Move, HashSet<Move>> MoveRules = new()
    {
        { Rock, [Scissors, Lizard] },
        { Paper, [Rock, Spock] },
        { Scissors, [Lizard, Paper] },
        { Lizard, [Paper, Spock] },
        { Spock, [Scissors, Rock] }
    };

    public int Compare(Move move, Move otherMove)
    {
        if (move == otherMove)
            return 0;

        if (MoveRules[move].Contains(otherMove))
            return 1; // move beats otherMove
        
        return -1;
    }
}