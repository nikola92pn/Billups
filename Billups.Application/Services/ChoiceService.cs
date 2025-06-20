using System.Collections.Immutable;
using Billups.Application.Constants;
using Billups.Application.Dtos;
using Billups.Application.Interfaces;
using Billups.Domain.Interfaces;

namespace Billups.Application.Services;

public class ChoiceService(IRandomNumberService randomNumberService, IGameModeProvider gameModeProvider) : IChoiceService
{
    public ImmutableList<ChoiceDto> GetAll()
        => Choices.GetAll(gameModeProvider.GetCurrent());

    public async Task<ChoiceDto> GetRandomChoiceAsync(CancellationToken cancellationToken)
    {
        var choices = GetAll();
        var randomNumber = await randomNumberService.GetRandomNumberAsync(cancellationToken);
        var index = randomNumber % choices.Count;
        return choices[index];
    }
}