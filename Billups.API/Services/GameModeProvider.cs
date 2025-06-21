using Billups.Domain.Interfaces;
using Billups.Domain.Models;

namespace Billups.Api.Services;

public class GameModeProvider(IHttpContextAccessor httpContextAccessor, ILogger<GameModeProvider> logger) : IGameModeProvider
{
    public GameMode GetCurrent()
    {
        var context = httpContextAccessor.HttpContext;

        if (context == null)
            throw new InvalidProgramException("HttpContext is missing.");

        var headerValue = context.Request.Headers["x-game-mode"].ToString();
        
        if (string.IsNullOrEmpty(headerValue))
            return GameMode.Rpsls; // default mode

        if (Enum.TryParse<GameMode>(headerValue, ignoreCase: true, out var mode))
        {
            logger.LogDebug("The header {Value} x-game-mode has been successfully parsed.", headerValue);
            return mode;
        }

        logger.LogWarning("The header {Value} x-game-mode could not be parsed.", headerValue);
        return GameMode.Rpsls; // default mode
    }
}