using PublicTransport.Core.Application.Models.Results;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Core.Application.Interfaces.Persistence;

public interface IVehicleRepository
{
    Task<RepositoryResult<Guid>> CreateAsync(Vehicle vehicle, CancellationToken cancellationToken = default);
    Task<RepositoryResult<Vehicle?>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<RepositoryResult<IReadOnlyCollection<Vehicle>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RepositoryResult> DeleteAsync(Vehicle vehicle, CancellationToken cancellationToken = default);
}