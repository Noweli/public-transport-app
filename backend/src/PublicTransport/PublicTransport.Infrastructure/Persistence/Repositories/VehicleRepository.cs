using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublicTransport.Core.Application.Interfaces.Persistence;
using PublicTransport.Core.Application.Models.Results;
using PublicTransport.Core.Application.Models.Results.Codes;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Infrastructure.Persistence.Repositories;

public sealed class VehicleRepository(ILogger<VehicleRepository> logger, ApplicationDbContext applicationDbContext)
    : IVehicleRepository
{
    public async Task<RepositoryResult<Guid>> CreateAsync(Vehicle vehicle,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var addedVehicle = await applicationDbContext.Vehicles.AddAsync(vehicle, cancellationToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return new RepositoryResult<Guid>(RepositoryResultCode.Created, addedVehicle.Entity.Id);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to create vehicle. Operation canceled.");
            return new RepositoryResult<Guid>(RepositoryResultCode.Cancelled, Guid.Empty);
        }
        catch (DbUpdateException exception)
        {
            logger.LogError(exception, "Failed to create vehicle. Database thrown exception.");
            return new RepositoryResult<Guid>(RepositoryResultCode.DbException, Guid.Empty);
        }
    }

    public async Task<RepositoryResult<Vehicle?>> GetByNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var vehicle =
                await applicationDbContext.Vehicles.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

            return vehicle is null
                ? new RepositoryResult<Vehicle?>(RepositoryResultCode.NotFound, null)
                : new RepositoryResult<Vehicle?>(RepositoryResultCode.Found, vehicle);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to get vehicle. Operation canceled.");
            return new RepositoryResult<Vehicle?>(RepositoryResultCode.Cancelled, null);
        }
        catch (ArgumentNullException exception)
        {
            logger.LogError(exception, "Failed to get vehicle. Source is null.");
            return new RepositoryResult<Vehicle?>(RepositoryResultCode.DbException, null);
        }
    }

    public async Task<RepositoryResult<IReadOnlyCollection<Vehicle>>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var vehicles = await applicationDbContext.Vehicles.ToListAsync(cancellationToken);
            return new RepositoryResult<IReadOnlyCollection<Vehicle>>(RepositoryResultCode.Found, vehicles);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to get all vehicles. Operation canceled.");
            return new RepositoryResult<IReadOnlyCollection<Vehicle>>(RepositoryResultCode.Cancelled, []);
        }
        catch (ArgumentNullException exception)
        {
            logger.LogError(exception, "Failed to get all vehicles. Source is null.");
            return new RepositoryResult<IReadOnlyCollection<Vehicle>>(RepositoryResultCode.DbException, []);
        }
    }

    public async Task<RepositoryResult> DeleteAsync(Vehicle vehicle, CancellationToken cancellationToken = default)
    {
        try
        {
            applicationDbContext.Vehicles.Remove(vehicle);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return new RepositoryResult(RepositoryResultCode.Removed);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to delete vehicle. Operation canceled.");
            return new RepositoryResult(RepositoryResultCode.Cancelled);
        }
        catch (DbUpdateException exception)
        {
            logger.LogError(exception, "Failed to delete vehicle. Database thrown exception.");
            return new RepositoryResult(RepositoryResultCode.DbException);
        }
    }
}