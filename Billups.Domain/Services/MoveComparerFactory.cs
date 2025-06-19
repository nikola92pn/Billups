using Billups.Domain.Interfaces;
using Billups.Domain.Models;

namespace Billups.Domain.Services;

internal class MoveComparerFactory(IMoveRulesProvider rulesProvider) : IMoveComparerFactory
{
    public IComparer<Move> GetComparer(GameMode mode)
    {
        var rules = rulesProvider.GetRules(mode);
        return new MoveComparer(rules);
    }
}