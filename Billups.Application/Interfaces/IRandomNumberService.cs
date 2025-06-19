namespace Billups.Application.Interfaces;

public interface IRandomNumberService
{
    public Task<int> GetRandomNumberAsync(CancellationToken cancellationToken);
}