using Billups.Domain.Models;

namespace Billups.Domain.Interfaces;

public interface IGameRulesService
{
    GameResult Beat(Move playerMove, Move otherMove);
}