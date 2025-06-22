using Billups.Api.Models;
using Billups.Application.Dtos;

namespace Billups.Api.Mappers;

public static class HistoryMapper
{
    public static HistoryResponse ToResponse(this GameHistoryDto historyDto)
    => new(historyDto.PlayerChoice.ToResponse(),  historyDto.CpuChoice.ToResponse(), historyDto.CreatedAt);
}