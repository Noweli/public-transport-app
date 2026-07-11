using PublicTransport.Core.Application.Models.Results;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Core.Application.Interfaces.Persistence;

public interface ILineRepository
{
    Task<RepositoryResult<Guid>> CreateAsync(Line line, CancellationToken cancellationToken = default);
    Task<RepositoryResult<Line?>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<RepositoryResult<IReadOnlyCollection<Line>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RepositoryResult> DeleteAsync(Line line, CancellationToken cancellationToken = default);
}