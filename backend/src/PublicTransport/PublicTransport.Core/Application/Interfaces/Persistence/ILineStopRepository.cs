using PublicTransport.Core.Domain.Models;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Core.Application.Interfaces.Persistence;

public interface ILineStopRepository
{
    Task<Result<LineStop>> CreateAsync(LineStop line, CancellationToken cancellationToken = default);
    Task<Result<ICollection<LineStop>>> GetByStopNameAsync(string stopName, CancellationToken cancellationToken = default);
    Task<Result<ICollection<LineStop>>> GetByLineNameAsync(string lineName, CancellationToken cancellationToken = default);
    Task<Result<ICollection<LineStop>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<LineStop>> DeleteAsync(LineStop line, CancellationToken cancellationToken = default);
}