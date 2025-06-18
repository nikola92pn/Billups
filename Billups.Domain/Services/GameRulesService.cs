using Billups.Domain.Interfaces;
using Billups.Domain.Models;

namespace Billups.Domain.Services;

internal class GameRulesService : IGameRulesService
{
    public GameResult Beat(Move playerMove, Move otherMove)
    {
        var moveComparer = new MoveComparer();
        var result = moveComparer.Compare(playerMove, otherMove);
        return MapToGameResult(result);
    }

    private static GameResult MapToGameResult(int result) =>
        result switch
        {
            1 => GameResult.Win,
            0 => GameResult.Tie,
            -1 => GameResult.Lose,
            _ => throw new ArgumentOutOfRangeException(nameof(result))
        };
}