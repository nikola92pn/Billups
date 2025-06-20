using System.Collections.Concurrent;
using System.Collections.Immutable;
using Billups.Application.Dtos;
using Billups.Application.Interfaces;
using Billups.Domain.Interfaces;
using Billups.Domain.Models;

namespace Billups.Application.Services;

public class ChoiceProvider(IMoveRulesProvider moveRuleProvider, IGameModeProvider gameModeProvider) : IChoiceProvider
{
    private readonly ConcurrentDictionary<GameMode, Lazy<ImmutableList<ChoiceDto>>> _choicesByMode = new();

    public ImmutableList<ChoiceDto> GetAll()
    {
        var mode = gameModeProvider.GetCurrent();

        if (!_choicesByMode.TryGetValue(mode, out var lazyChoices))
        {
            lazyChoices = new Lazy<ImmutableList<ChoiceDto>>(() =>
            {
                var moves = moveRuleProvider.GetRules(mode).Keys;
                return moves
                    .Select(move => new ChoiceDto((int)move + 1, move))
                    .ToImmutableList();
            });

            _choicesByMode[mode] = lazyChoices;
        }

        return lazyChoices.Value;
    }
    
    public ChoiceDto GetChoice(int choiceId) 
        => GetAll().First(c => c.Id == choiceId);
    
    public ChoiceDto GetChoice(Move move) 
        => GetAll().First(c => c.Move == move);
}