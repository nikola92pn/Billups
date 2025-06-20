using System.Collections.Immutable;
using Billups.Application.Dtos;
using Billups.Domain.Models;
using Billups.Domain.Rules;

namespace Billups.Application.Constants;

internal static class Choices
{
    private static ImmutableList<ChoiceDto> _currentGameModeChoices = ImmutableList<ChoiceDto>.Empty;

    internal static ImmutableList<ChoiceDto> GetAll(GameMode mode)
    {
        var moves = GameModeMoves.MovesByMode[mode];
        _currentGameModeChoices = moves
            .Select(e => new ChoiceDto((int)e + 1, e))
            .ToImmutableList();
        return _currentGameModeChoices;
    }
    
    internal static ChoiceDto GetChoice(int choiceId) 
        => _currentGameModeChoices.First(c => c.Id == choiceId);
    
    internal static ChoiceDto GetChoice(Move move) 
        => _currentGameModeChoices.First(c => c.Move == move);
}