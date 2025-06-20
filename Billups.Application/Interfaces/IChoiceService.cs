using System.Collections.Immutable;
using Billups.Application.Dtos;

namespace Billups.Application.Interfaces;

public interface IChoiceService
{
    ImmutableList<ChoiceDto> GetAll();
    Task<ChoiceDto> GetRandomChoiceAsync(CancellationToken cancellationToken);
}