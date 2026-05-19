using PublicTransport.Core.Domain.Models;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Core.Application.Interfaces.Persistence;

public interface IVehicleRepository
{
    Task<Result<Vehicle>> CreateAsync(Vehicle vehicle, CancellationToken cancellationToken = default);
    Task<Result<Vehicle>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyCollection<Vehicle>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<Vehicle>> DeleteAsync(Vehicle vehicle, CancellationToken cancellationToken = default);
}