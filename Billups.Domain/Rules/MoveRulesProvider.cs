using System.Collections.Immutable;
using Billups.Domain.Interfaces;
using Billups.Domain.Models;
using static Billups.Domain.Models.Move;

namespace Billups.Domain.Rules;

public class MoveRulesProvider : IMoveRulesProvider
{
    public ImmutableDictionary<Move, ImmutableHashSet<Move>> GetRules(GameMode mode) =>
        mode switch
        {
            GameMode.Rps => ImmutableDictionary.CreateRange([
                KeyValuePair.Create(Rock, ImmutableHashSet.Create(Scissors)),
                KeyValuePair.Create(Paper, ImmutableHashSet.Create(Rock)),
                KeyValuePair.Create(Scissors, ImmutableHashSet.Create(Paper))
            ]),
            GameMode.Rpsls => ImmutableDictionary.CreateRange([
                KeyValuePair.Create(Rock, ImmutableHashSet.Create(Scissors, Lizard)),
                KeyValuePair.Create(Paper, ImmutableHashSet.Create(Rock, Spock)),
                KeyValuePair.Create(Scissors, ImmutableHashSet.Create(Paper, Lizard)),
                KeyValuePair.Create(Lizard, ImmutableHashSet.Create(Spock, Paper)),
                KeyValuePair.Create(Spock, ImmutableHashSet.Create(Scissors, Rock))
            ]),
            _ => throw new ArgumentOutOfRangeException(nameof(mode))
        };
}