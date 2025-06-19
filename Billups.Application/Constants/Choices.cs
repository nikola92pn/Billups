using System.Collections.Immutable;
using Billups.Application.Dtos;
using Billups.Domain.Models;

namespace Billups.Application.Constants;

internal static class Choices
{
    internal static readonly ImmutableList<ChoiceDto> All =
        Enum.GetValues<Move>()
            .Select(e => new ChoiceDto((int)e + 1, e))
            .ToImmutableList();
}