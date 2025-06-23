using Billups.Domain.Models;

namespace Billups.Application.Dtos;

public record GameHistoryDto(GameResult Result, ChoiceDto PlayerChoice, ChoiceDto CpuChoice, GameMode GameMode, DateTime CreatedAt);