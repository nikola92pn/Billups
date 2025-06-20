using System.Collections.Immutable;
using Billups.Application.Dtos;
using Billups.Domain.Models;

namespace Billups.Application.Interfaces;

public interface IChoiceProvider
{
    ImmutableList<ChoiceDto> GetAll();
    ChoiceDto GetChoice(int choiceId);
    ChoiceDto GetChoice(Move move);
}