using Billups.Application.Dtos;
using Billups.Application.Interfaces;
using Billups.Application.Services;
using Billups.Domain.Interfaces;
using Billups.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Billups.UnitTests;

public class GameServiceTests
{
    private readonly Mock<IRandomChoiceGenerator> _randomChoiceGenerator = new();
    private readonly Mock<IGameRulesService> _gameRulesService = new();
    private readonly Mock<IGameHistoryService> _gameHistoryService = new();
    private readonly Mock<ICurrentChoiceResolver> _currentChoiceResolver = new();
    private readonly Mock<ILogger<GameService>> _logger = new();

    private GameService CreateService() =>
        new(
            _randomChoiceGenerator.Object,
            _gameRulesService.Object,
            _gameHistoryService.Object,
            _currentChoiceResolver.Object,
            _logger.Object);

    [Fact]
    public async Task PlayAgainstCpuAsync_ReturnsGameResultDto()
    {
        // Arrange\
        var playerChoiceId = 1;
        var playerChoice = new ChoiceDto(playerChoiceId, Move.Rock);
        var cpuChoice = new ChoiceDto(2, Move.Paper);
        var expectedResult = GameResult.Lose;

        _currentChoiceResolver.Setup(x => x.GetChoice(playerChoiceId)).Returns(playerChoice);
        _randomChoiceGenerator.Setup(x => x.GetAsync(It.IsAny<CancellationToken>())).ReturnsAsync(cpuChoice);
        _gameRulesService.Setup(x => x.Beat(playerChoice.Move, cpuChoice.Move)).Returns(expectedResult);
        _gameHistoryService.Setup(x => x.SaveAsync(It.IsAny<GameHistoryDto>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var service = CreateService();

        // Act
        var result = await service.PlayAgainstCpuAsync(playerChoiceId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result.Result);
        Assert.Equal(playerChoice.Id, result.PlayerChoice.Id);
        Assert.Equal(cpuChoice.Id, result.CpuChoice.Id);

        // Optionally verify SaveAsync was called
        _gameHistoryService.Verify(x => x.SaveAsync(It.IsAny<GameHistoryDto>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}