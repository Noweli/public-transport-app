using PublicTransport.Core.Domain.Models;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Core.Application.Interfaces.Persistence;

public interface IStopRepository
{
    Task<Result<Stop>> CreateAsync(Stop stop, CancellationToken cancellationToken = default);
    Task<Result<Stop>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<Result<Stop>> GetByStreetAsync(string street, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyCollection<Stop>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<Stop>> DeleteAsync(Stop stop, CancellationToken cancellationToken = default);
}