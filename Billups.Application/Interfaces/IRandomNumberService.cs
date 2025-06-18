namespace Billups.Application.Interfaces;

public interface IRandomNumberService
{
    public Task<int> GetRandomNumber(CancellationToken cancellationToken);
}