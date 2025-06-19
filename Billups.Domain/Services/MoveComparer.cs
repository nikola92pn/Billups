using Billups.Domain.Models;

namespace Billups.Domain.Services;

internal class MoveComparer(Dictionary<Move, HashSet<Move>> moveRules) : IComparer<Move>
{
    public int Compare(Move move, Move otherMove)
    {
        if (move == otherMove)
            return 0;

        if (moveRules[move].Contains(otherMove))
            return 1; // move beats otherMove

        return -1;
    }
}