using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublicTransport.Core.Application.Interfaces.Persistence;
using PublicTransport.Core.Application.Models.Results;
using PublicTransport.Core.Application.Models.Results.Codes;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Infrastructure.Persistence.Repositories;

public sealed class LineStopRepository(ILogger<LineStopRepository> logger, ApplicationDbContext applicationDbContext)
    : ILineStopRepository
{
    public async Task<RepositoryResult<Guid>> CreateAsync(LineStop lineStop,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var addedLineStop = await applicationDbContext.LineStops.AddAsync(lineStop, cancellationToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return new RepositoryResult<Guid>(RepositoryResultCode.Created, addedLineStop.Entity.Id);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to create line stop. Operation canceled.");
            return new RepositoryResult<Guid>(RepositoryResultCode.Cancelled, Guid.Empty);
        }
        catch (DbUpdateException exception)
        {
            logger.LogError(exception, "Failed to create line stop. Database thrown exception.");
            return new RepositoryResult<Guid>(RepositoryResultCode.DbException, Guid.Empty);
        }
    }

    public async Task<RepositoryResult<LineStop?>> GetByStopNameAsync(string stopName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var lineStop = await applicationDbContext.LineStops
                .Include(lineStop => lineStop.Stop)
                .FirstOrDefaultAsync(lineStop => lineStop.Stop != null && lineStop.Stop.Name == stopName,
                    cancellationToken);

            return lineStop is null
                ? new RepositoryResult<LineStop?>(RepositoryResultCode.NotFound, null)
                : new RepositoryResult<LineStop?>(RepositoryResultCode.Found, lineStop);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to get line stop by stop name. Operation canceled.");
            return new RepositoryResult<LineStop?>(RepositoryResultCode.Cancelled, null);
        }
        catch (ArgumentNullException exception)
        {
            logger.LogError(exception, "Failed to get line stop by stop name. Source is null.");
            return new RepositoryResult<LineStop?>(RepositoryResultCode.DbException, null);
        }
    }

    public async Task<RepositoryResult<LineStop?>> GetByLineNameAsync(string lineName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var lineStop = await applicationDbContext.LineStops
                .Include(lineStop => lineStop.Line)
                .FirstOrDefaultAsync(lineStop => lineStop.Line != null && lineStop.Line.Name == lineName,
                    cancellationToken);

            return lineStop is null
                ? new RepositoryResult<LineStop?>(RepositoryResultCode.NotFound, null)
                : new RepositoryResult<LineStop?>(RepositoryResultCode.Found, lineStop);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to get line stop by line name. Operation canceled.");
            return new RepositoryResult<LineStop?>(RepositoryResultCode.Cancelled, null);
        }
        catch (ArgumentNullException exception)
        {
            logger.LogError(exception, "Failed to get line stop by line name. Source is null.");
            return new RepositoryResult<LineStop?>(RepositoryResultCode.DbException, null);
        }
    }

    public async Task<RepositoryResult<IReadOnlyCollection<LineStop>>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var lineStops = await applicationDbContext.LineStops.ToListAsync(cancellationToken);
            return new RepositoryResult<IReadOnlyCollection<LineStop>>(RepositoryResultCode.Found, lineStops);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to get all line stops. Operation canceled.");
            return new RepositoryResult<IReadOnlyCollection<LineStop>>(RepositoryResultCode.Cancelled, []);
        }
        catch (ArgumentNullException exception)
        {
            logger.LogError(exception, "Failed to get all line stops. Source is null.");
            return new RepositoryResult<IReadOnlyCollection<LineStop>>(RepositoryResultCode.DbException, []);
        }
    }

    public async Task<RepositoryResult> DeleteAsync(LineStop lineStop, CancellationToken cancellationToken = default)
    {
        try
        {
            applicationDbContext.LineStops.Remove(lineStop);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return new RepositoryResult(RepositoryResultCode.Removed);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to delete line stop. Operation canceled.");
            return new RepositoryResult(RepositoryResultCode.Cancelled);
        }
        catch (DbUpdateException exception)
        {
            logger.LogError(exception, "Failed to delete line stop. Database thrown exception.");
            return new RepositoryResult(RepositoryResultCode.DbException);
        }
    }
}