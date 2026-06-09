using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublicTransport.Core.Application.Interfaces.Persistence;
using PublicTransport.Core.Application.Models.Results;
using PublicTransport.Core.Application.Models.Results.Codes;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Infrastructure.Persistence.Repositories;

public sealed class StopRepository(ILogger<StopRepository> logger, ApplicationDbContext applicationDbContext)
    : IStopRepository
{
    public async Task<RepositoryResult<Guid>> CreateAsync(Stop stop, CancellationToken cancellationToken = default)
    {
        try
        {
            var addedStop = await applicationDbContext.Stops.AddAsync(stop, cancellationToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return new RepositoryResult<Guid>(RepositoryResultCode.Success, addedStop.Entity.Id);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to create stop. Operation canceled.");
            return new RepositoryResult<Guid>(RepositoryResultCode.Cancelled, Guid.Empty);
        }
        catch (DbUpdateException exception)
        {
            logger.LogError(exception, "Failed to create stop. Database thrown exception.");
            return new RepositoryResult<Guid>(RepositoryResultCode.DbException, Guid.Empty);
        }
    }

    public async Task<RepositoryResult<Stop?>> GetByNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var stop = await applicationDbContext.Stops.FirstOrDefaultAsync(
                stop => stop.Name == name, cancellationToken);

            return stop is null
                ? new RepositoryResult<Stop?>(RepositoryResultCode.NotFound, null)
                : new RepositoryResult<Stop?>(RepositoryResultCode.Success, stop);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to get stop by name. Operation canceled.");
            return new RepositoryResult<Stop?>(RepositoryResultCode.Cancelled, null);
        }
        catch (ArgumentNullException exception)
        {
            logger.LogError(exception, "Failed to get stop by name. Source is null.");
            return new RepositoryResult<Stop?>(RepositoryResultCode.DbException, null);
        }
    }

    public async Task<RepositoryResult<IReadOnlyCollection<Stop>>> GetByStreetAsync(string street,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var stops = await applicationDbContext.Stops.Where(
                stop => stop.Street != null && stop.Street == street)
                .ToListAsync(cancellationToken);
            
            return new RepositoryResult<IReadOnlyCollection<Stop>>(RepositoryResultCode.Success, stops);

        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to get stop by street. Operation canceled.");
            return new RepositoryResult<IReadOnlyCollection<Stop>>(RepositoryResultCode.Cancelled, []);
        }
        catch (ArgumentNullException exception)
        {
            logger.LogError(exception, "Failed to get stop by street. Source is null.");
            return new RepositoryResult<IReadOnlyCollection<Stop>>(RepositoryResultCode.DbException, []);
        }
    }

    public async Task<RepositoryResult<IReadOnlyCollection<Stop>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var stops = await applicationDbContext.Stops.ToListAsync(cancellationToken);
            return new RepositoryResult<IReadOnlyCollection<Stop>>(RepositoryResultCode.Success, stops);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to get all stops. Operation canceled.");
            return new RepositoryResult<IReadOnlyCollection<Stop>>(RepositoryResultCode.Cancelled, []);
        }
        catch (ArgumentNullException exception)
        {
            logger.LogError(exception, "Failed to get all stops. Source is null.");
            return new RepositoryResult<IReadOnlyCollection<Stop>>(RepositoryResultCode.DbException, []);
        }
    }

    public async Task<RepositoryResult> DeleteAsync(Stop stop, CancellationToken cancellationToken = default)
    {
        try
        {
            applicationDbContext.Stops.Remove(stop);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return new RepositoryResult(RepositoryResultCode.Success);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to delete stop. Operation canceled.");
            return new RepositoryResult(RepositoryResultCode.Cancelled);
        }
        catch (DbUpdateException exception)
        {
            logger.LogError(exception, "Failed to delete stop. Database thrown exception.");
            return new RepositoryResult(RepositoryResultCode.DbException);
        }
    }
}