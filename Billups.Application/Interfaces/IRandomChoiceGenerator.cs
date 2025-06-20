using Billups.Application.Dtos;

namespace Billups.Application.Interfaces;

public interface IRandomChoiceGenerator
{
    Task<ChoiceDto> GetAsync(CancellationToken cancellationToken);
}