using System.Collections.Immutable;
using Billups.Application.Dtos;
using Billups.Domain.Models;

namespace Billups.Application.Interfaces;

/// <summary>
/// Resolves available player choices based on the current game mode context.
/// </summary>
public interface ICurrentChoiceResolver
{
    /// <summary>
    /// Gets all available choices for the current game mode.
    /// </summary>
    /// <returns>
    /// An immutable list of <see cref="ChoiceDto"/> representing the possible moves.
    /// </returns>
    ImmutableList<ChoiceDto> GetAll();
    
    /// <summary>
    /// Retrieves a specific choice by its unique identifier.
    /// </summary>
    /// <param name="choiceId">The ID of the choice to retrieve.</param>
    /// <returns>The matching <see cref="ChoiceDto"/> if found; otherwise, throws if not found.</returns>
    ChoiceDto GetChoice(int choiceId);
    
    /// <summary>
    /// Retrieves a specific choice by its associated move.
    /// </summary>
    /// <param name="move">The move associated with the desired choice.</param>
    /// <returns>The matching <see cref="ChoiceDto"/> for the given move.</returns>
    ChoiceDto GetChoice(Move move);
}