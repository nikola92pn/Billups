using System.Collections.Immutable;
using Billups.Domain.Models;

namespace Billups.Domain.Interfaces;

public interface IMoveRulesProvider
{
    ImmutableDictionary<Move, ImmutableHashSet<Move>> GetRules(GameMode mode);
}