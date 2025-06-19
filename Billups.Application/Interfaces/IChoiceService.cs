using System.Collections.Immutable;
using Billups.Application.Dtos;

namespace Billups.Application.Interfaces;

public interface IChoiceService
{
    ImmutableList<ChoiceDto> GetAll();
    ChoiceDto GetChoice(int choiceId);
    Task<ChoiceDto> GetRandomChoiceAsync(CancellationToken cancellationToken);
}