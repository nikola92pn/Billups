using Billups.Domain.Models;

namespace Billups.Application.Interfaces;

public interface IGameService
{
    public Task Play(Move playerMove, CancellationToken cancellationToken);
}