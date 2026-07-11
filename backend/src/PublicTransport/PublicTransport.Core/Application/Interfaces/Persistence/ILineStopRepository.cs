using PublicTransport.Core.Application.Models.Results;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Core.Application.Interfaces.Persistence;

public interface ILineStopRepository
{
    Task<RepositoryResult<Guid>> CreateAsync(LineStop lineStop, CancellationToken cancellationToken = default);
    Task<RepositoryResult<LineStop?>> GetByStopNameAsync(string stopName, CancellationToken cancellationToken = default);
    Task<RepositoryResult<LineStop?>> GetByLineNameAsync(string lineName, CancellationToken cancellationToken = default);
    Task<RepositoryResult<IReadOnlyCollection<LineStop>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RepositoryResult> DeleteAsync(LineStop lineStop, CancellationToken cancellationToken = default);
}