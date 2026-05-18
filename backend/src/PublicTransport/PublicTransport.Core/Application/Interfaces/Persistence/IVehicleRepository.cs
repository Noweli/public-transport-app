using PublicTransport.Core.Domain.Models;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Core.Application.Interfaces.Persistence;

public interface IVehicleRepository
{
    Task<Result<Vehicle>> CreateAsync(Vehicle vehicle);
    Task<Result<Vehicle>> GetAsync(string name);
    Task<Result<ICollection<Vehicle>>> GetAllAsync();
    Task<Result<Vehicle>> DeleteAsync(string name);
}