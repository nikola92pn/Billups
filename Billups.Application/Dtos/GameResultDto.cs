using Billups.Domain.Models;

namespace Billups.Application.Dtos;

public record GameResultDto(GameResult Result, ChoiceDto PlayerChoice, ChoiceDto CpuChoice);