using System.Collections.Immutable;
using Billups.Application.Dtos;
using Billups.Application.Interfaces;
using Billups.Application.Services;
using Billups.Domain.Interfaces;
using Billups.Domain.Models;
using Moq;
using Xunit;

namespace Billups.UnitTests;

public class CurrentChoiceResolverTests
{
    [Fact]
    public void GetChoice_ReturnsCorrectChoice_ForGivenModeAndId()
    {
        // Arrange
        var mockGameModeProvider = new Mock<IGameModeProvider>();
        var mockChoiceCache = new Mock<IChoiceCache>();
        mockGameModeProvider.Setup(m => m.GetCurrent()).Returns(GameMode.Rpsls);

        var expectedChoices = ImmutableList.Create(
            new ChoiceDto(1, Move.Rock),
            new ChoiceDto(2, Move.Paper),
            new ChoiceDto(3, Move.Scissors),
            new ChoiceDto(4, Move.Lizard),
            new ChoiceDto(5, Move.Spock)
        );
        mockChoiceCache.Setup(c => c.GetChoices(GameMode.Rpsls)).Returns(expectedChoices);
        
        // Act
        var resolver = new CurrentChoiceResolver(mockChoiceCache.Object, mockGameModeProvider.Object);
        var result = resolver.GetChoice(2);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Id);
        Assert.Equal(Move.Paper, result.Move);
    }

    [Fact]
    public void GetChoice_ThrowsException_IfChoiceIdNotFound()
    {
        // Arrange
        var mockGameModeProvider = new Mock<IGameModeProvider>();
        var mockChoiceCache = new Mock<IChoiceCache>();
        mockGameModeProvider.Setup(m => m.GetCurrent()).Returns(GameMode.Rps);
        
        var expectedChoices = ImmutableList.Create(
            new ChoiceDto(1, Move.Rock),
            new ChoiceDto(2, Move.Paper),
            new ChoiceDto(3, Move.Scissors)
        );
        mockChoiceCache.Setup(c => c.GetChoices(GameMode.Rps)).Returns(expectedChoices);

        var resolver = new CurrentChoiceResolver(mockChoiceCache.Object, mockGameModeProvider.Object);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => resolver.GetChoice(5)); // 5 doesn't exist in Rps mode
    }
}