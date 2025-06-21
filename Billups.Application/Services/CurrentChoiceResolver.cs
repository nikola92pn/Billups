using System.Collections.Immutable;
using Billups.Application.Dtos;
using Billups.Application.Interfaces;
using Billups.Domain.Interfaces;
using Billups.Domain.Models;

namespace Billups.Application.Services;

public class CurrentChoiceResolver(IChoiceCache choiceCache, IGameModeProvider gameModeProvider) : ICurrentChoiceResolver
{
    public ImmutableList<ChoiceDto> GetAll()
    {
        var mode = gameModeProvider.GetCurrent();
        return choiceCache.GetChoices(mode);
    }
    
    public ChoiceDto GetChoice(int choiceId) 
        => GetAll().First(c => c.Id == choiceId);
    
    public ChoiceDto GetChoice(Move move) 
        => GetAll().First(c => c.Move == move);
}