using Billups.Domain.Models;

namespace Billups.Api.Configuration;

public class GameSettings
{
    public GameMode Mode { get; set; } = GameMode.Rpsls; // default mode
}