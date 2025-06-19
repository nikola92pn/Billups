using Billups.Domain.Models;

namespace Billups.Domain.Interfaces;

internal interface IMoveRulesProvider
{
    Dictionary<Move, HashSet<Move>> GetRules(GameMode mode);
}