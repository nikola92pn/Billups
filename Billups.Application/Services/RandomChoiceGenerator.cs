using Billups.Application.Dtos;
using Billups.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Billups.Application.Services;

public class RandomChoiceGenerator(IRandomNumberService randomNumberService, ICurrentChoiceResolver currentChoiceResolver, ILogger<RandomChoiceGenerator> logger) : IRandomChoiceGenerator
{
    public async Task<ChoiceDto> GetAsync(CancellationToken cancellationToken)
    {
        var choices = currentChoiceResolver.GetAll();
        logger.LogDebug("Retrieved {Count} available choices for current game mode.", choices.Count);
        
        var randomNumber = await randomNumberService.GetRandomNumberAsync(cancellationToken);
        logger.LogDebug("Generated random number: {RandomNumber}", randomNumber);
        
        var index = randomNumber % choices.Count;
        var choice = choices[index];
        
        logger.LogInformation("Random choice selected: {ChoiceId} - {Move}", choice.Id, choice.Move);
        return choice;
    }
}