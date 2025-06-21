using System.Collections.Immutable;
using Billups.Application.Dtos;
using Billups.Domain.Models;

namespace Billups.Application.Interfaces;

/// <summary>
/// Provides a cached collection of available player choices for a given game mode.
/// </summary>
public interface IChoiceCache
{
    /// <summary>
    /// Retrieves the list of choices available for the specified <see cref="GameMode"/>.
    /// </summary>
    /// <param name="mode">The game mode for which to retrieve the choices.</param>
    /// <returns>
    /// An immutable list of <see cref="ChoiceDto"/> objects representing the possible moves in the given game mode.
    /// </returns>
    ImmutableList<ChoiceDto> GetChoices(GameMode mode);
}