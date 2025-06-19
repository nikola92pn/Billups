using Billups.Application.Constants;
using Billups.Application.Dtos;
using Billups.Application.Interfaces;
using Billups.Domain.Models;

namespace Billups.Application.Services;

public class ChoiceService(IRandomNumberService randomNumberService) : IChoiceService
{
    public IReadOnlyList<ChoiceDto> GetAll()
        => Choices.All;

    public async Task<ChoiceDto> GetRandomChoiceAsync(CancellationToken cancellationToken)
    {
        var randomMove = await GetRandomMoveAsync(cancellationToken);
        return Choices.All.First(c => c.Move == randomMove);

        async Task<Move> GetRandomMoveAsync(CancellationToken cancellationToken1)
        {
            var randomNumber = await randomNumberService.GetRandomNumberAsync(cancellationToken1);
            var moves = Enum.GetValues<Move>();
            var index = randomNumber % moves.Length;
            return moves[index];
        }
    }

    public ChoiceDto GetChoice(int choiceId) 
        => Choices.All.First(c => c.Id == choiceId);
}