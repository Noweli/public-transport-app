namespace PublicTransport.Core.Application.UseCases;

public interface IUseCase<T>
{
    Task<T> ExecuteAsync(CancellationToken cancellationToken = default);
}