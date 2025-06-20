using Billups.Application.Constants;
using Billups.Application.Dtos;
using Billups.Domain.Models;

namespace Billups.Application.Mappers;

public static class HistoryMapper
{
    public static GameHistory ToGameHistory(this GameHistoryDto history)
        => new(history.Result, history.PlayerChoice.Move, history.CpuChoice.Move, history.CreatedAt);

    public static GameHistoryDto ToGameHistoryDto(this GameHistory history)
        => new(history.Result, Choices.GetChoice(history.PlayerMove), Choices.GetChoice(history.CpuMove), history.CreatedAt);
}