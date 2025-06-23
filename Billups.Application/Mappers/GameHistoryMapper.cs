using Billups.Application.Dtos;
using Billups.Application.Interfaces;
using Billups.Domain.Models;

namespace Billups.Application.Mappers;

public class GameHistoryMapper(ICurrentChoiceResolver currentChoiceResolver) : IGameHistoryMapper
{
    public GameHistory ToDomain(GameHistoryDto dto)
        => new(dto.Result, dto.PlayerChoice.Move, dto.CpuChoice.Move, dto.GameMode, dto.CreatedAt);

    public GameHistoryDto ToDto(GameHistory history)
        => new(
            history.Result,
            currentChoiceResolver.GetChoice(history.PlayerMove),
            currentChoiceResolver.GetChoice(history.CpuMove),
            history.GameMode,
            history.CreatedAt
        );
}