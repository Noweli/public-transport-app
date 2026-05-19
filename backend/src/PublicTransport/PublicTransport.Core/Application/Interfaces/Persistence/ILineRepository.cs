using PublicTransport.Core.Domain.Models;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Core.Application.Interfaces.Persistence;

public interface ILineRepository
{
    Task<Result<Line>> CreateAsync(Line line, CancellationToken cancellationToken = default);
    Task<Result<Line>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyCollection<Line>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<Line>> DeleteAsync(Line line, CancellationToken cancellationToken = default);
}