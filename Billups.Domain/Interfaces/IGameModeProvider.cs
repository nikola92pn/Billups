using Billups.Domain.Models;

namespace Billups.Domain.Interfaces;

public interface IGameModeProvider
{
    GameMode GetCurrent();
}