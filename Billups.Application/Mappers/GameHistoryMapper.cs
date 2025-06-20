using Billups.Application.Dtos;
using Billups.Application.Interfaces;
using Billups.Domain.Models;

namespace Billups.Application.Mappers;

public class GameHistoryMapper(IChoiceProvider choiceProvider) : IGameHistoryMapper
{
    public GameHistory ToDomain(GameHistoryDto dto)
        => new(dto.Result, dto.PlayerChoice.Move, dto.CpuChoice.Move, dto.CreatedAt);

    public GameHistoryDto ToDto(GameHistory history)
        => new(
            history.Result,
            choiceProvider.GetChoice(history.PlayerMove),
            choiceProvider.GetChoice(history.CpuMove),
            history.CreatedAt
        );
}