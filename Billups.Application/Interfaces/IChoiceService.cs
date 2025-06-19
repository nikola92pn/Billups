using Billups.Application.Dtos;

namespace Billups.Application.Interfaces;

public interface IChoiceService
{
    IReadOnlyList<ChoiceDto> GetAll();
    ChoiceDto GetChoice(int choiceId);
    Task<ChoiceDto> GetRandomChoiceAsync(CancellationToken cancellationToken);
}