using Billups.Api.Configuration;
using Billups.Domain.Interfaces;
using Billups.Domain.Models;
using Microsoft.Extensions.Options;

namespace Billups.Api.Services;

public class GameModeProvider(IOptions<GameSettings> settings) : IGameModeProvider
{
    public GameMode GetCurrent() => settings.Value.Mode;
}