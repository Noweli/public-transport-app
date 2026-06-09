using PublicTransport.Core.Application.Models.Results;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Core.Application.Interfaces.Persistence;

public interface IStopRepository
{
    Task<RepositoryResult<Guid>> CreateAsync(Stop stop, CancellationToken cancellationToken = default);
    Task<RepositoryResult<Stop?>> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<RepositoryResult<IReadOnlyCollection<Stop>>> GetByStreetAsync(string street,
        CancellationToken cancellationToken = default);

    Task<RepositoryResult<IReadOnlyCollection<Stop>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RepositoryResult> DeleteAsync(Stop stop, CancellationToken cancellationToken = default);
}