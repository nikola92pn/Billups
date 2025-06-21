using System.Collections.Concurrent;
using System.Collections.Immutable;
using Billups.Application.Dtos;
using Billups.Application.Interfaces;
using Billups.Domain.Interfaces;
using Billups.Domain.Models;

namespace Billups.Application.Services;

public class ChoiceCache(IMoveRulesProvider rulesProvider) : IChoiceCache
{
    private readonly ConcurrentDictionary<GameMode, Lazy<ImmutableList<ChoiceDto>>> _choicesByMode = new();

    public ImmutableList<ChoiceDto> GetChoices(GameMode mode) =>
        _choicesByMode.GetOrAdd(mode, m => new Lazy<ImmutableList<ChoiceDto>>(() =>
        {
            var moves = rulesProvider.GetRules(m).Keys;
            return moves.Select(move => new ChoiceDto((int)move + 1, move)).ToImmutableList();
        })).Value;
}