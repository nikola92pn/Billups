namespace Billups.Domain.Models;

public record GameHistory(GameResult Result, Move PlayerMove, Move CpuMove, GameMode GameMode, DateTime CreatedAt);