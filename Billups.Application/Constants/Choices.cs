using System.Collections.Immutable;
using Billups.Application.Dtos;
using Billups.Domain.Models;
using Billups.Domain.Rules;

namespace Billups.Application.Constants;

internal static class Choices
{
    internal static ImmutableList<ChoiceDto> GetAll(GameMode mode)
    {
        var moves = GameModeMoves.MovesByMode[mode];
        return moves
            .Select(e => new ChoiceDto((int)e + 1, e))
            .ToImmutableList();
    }
        
}