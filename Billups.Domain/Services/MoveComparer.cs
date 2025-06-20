using System.Collections.Immutable;
using Billups.Domain.Models;

namespace Billups.Domain.Services;

internal class MoveComparer(ImmutableDictionary<Move, ImmutableHashSet<Move>> moveRules) : IComparer<Move>
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