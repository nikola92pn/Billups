using Billups.Application.Dtos;
using Billups.Domain.Models;

namespace Billups.Application.Interfaces;

public interface IGameHistoryMapper
{
    GameHistory ToDomain(GameHistoryDto dto);
    GameHistoryDto ToDto(GameHistory history);
}