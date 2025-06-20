using Billups.Domain.Models;

namespace Billups.Application.Dtos;

public record GameHistoryDto(GameResult Result, ChoiceDto PlayerChoice, ChoiceDto CpuChoice, DateTime CreatedAt);