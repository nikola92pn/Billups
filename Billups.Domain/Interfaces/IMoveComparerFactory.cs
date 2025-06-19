using Billups.Domain.Models;

namespace Billups.Domain.Interfaces;

internal interface IMoveComparerFactory
{
    IComparer<Move> GetComparer(GameMode mode);
}