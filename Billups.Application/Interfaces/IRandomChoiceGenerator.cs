using Billups.Application.Dtos;

namespace Billups.Application.Interfaces;

/// <summary>
/// Provides functionality to generate a random choice for the current game mode.
/// </summary>
public interface IRandomChoiceGenerator
{
    /// <summary>
    /// Generates a random choice for the current game mode.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>
    /// A task representing the operation. The task result contains a randomly selected <see cref="ChoiceDto"/>.
    /// </returns>
    Task<ChoiceDto> GetAsync(CancellationToken cancellationToken);
}