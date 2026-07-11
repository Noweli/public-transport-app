using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublicTransport.Core.Application.Interfaces.Persistence;
using PublicTransport.Core.Application.Models.Results;
using PublicTransport.Core.Application.Models.Results.Codes;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Infrastructure.Persistence.Repositories;

public sealed class LineRepository(ILogger<LineRepository> logger, ApplicationDbContext applicationDbContext)
    : ILineRepository
{
    public async Task<RepositoryResult<Guid>> CreateAsync(Line line, CancellationToken cancellationToken = default)
    {
        try
        {
            var addedLine = await applicationDbContext.Lines.AddAsync(line, cancellationToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return new RepositoryResult<Guid>(RepositoryResultCode.Created, addedLine.Entity.Id);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to create line. Operation canceled.");
            return new RepositoryResult<Guid>(RepositoryResultCode.Cancelled, Guid.Empty);
        }
        catch (DbUpdateException exception)
        {
            logger.LogError(exception, "Failed to create line. Database thrown exception.");
            return new RepositoryResult<Guid>(RepositoryResultCode.DbException, Guid.Empty);
        }
    }

    public async Task<RepositoryResult<Line?>> GetByNameAsync(string name,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var line = await applicationDbContext.Lines.FirstOrDefaultAsync(
                line => line.Name == name, cancellationToken);

            return line is null
                ? new RepositoryResult<Line?>(RepositoryResultCode.NotFound, null)
                : new RepositoryResult<Line?>(RepositoryResultCode.Found, line);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to get line by name. Operation canceled.");
            return new RepositoryResult<Line?>(RepositoryResultCode.Cancelled, null);
        }
        catch (ArgumentNullException exception)
        {
            logger.LogError(exception, "Failed to get line by name. Source is null.");
            return new RepositoryResult<Line?>(RepositoryResultCode.DbException, null);
        }
    }

    public async Task<RepositoryResult<IReadOnlyCollection<Line>>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var lines = await applicationDbContext.Lines.ToListAsync(cancellationToken);
            return new RepositoryResult<IReadOnlyCollection<Line>>(RepositoryResultCode.Found, lines);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to get all lines. Operation canceled.");
            return new RepositoryResult<IReadOnlyCollection<Line>>(RepositoryResultCode.Cancelled, []);
        }
        catch (ArgumentNullException exception)
        {
            logger.LogError(exception, "Failed to get all lines. Source is null.");
            return new RepositoryResult<IReadOnlyCollection<Line>>(RepositoryResultCode.DbException, []);
        }
    }

    public async Task<RepositoryResult> DeleteAsync(Line line, CancellationToken cancellationToken = default)
    {
        try
        {
            applicationDbContext.Lines.Remove(line);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return new RepositoryResult(RepositoryResultCode.Removed);
        }
        catch (OperationCanceledException exception)
        {
            logger.LogError(exception, "Failed to delete line. Operation canceled.");
            return new RepositoryResult(RepositoryResultCode.Cancelled);
        }
        catch (DbUpdateException exception)
        {
            logger.LogError(exception, "Failed to delete line. Database thrown exception.");
            return new RepositoryResult(RepositoryResultCode.DbException);
        }
    }
}