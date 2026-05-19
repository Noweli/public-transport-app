using PublicTransport.Core.Application.Models.Results;
using PublicTransport.Core.Domain.Models;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Core.Application.Interfaces.Persistence;

public interface IUserRepository
{
    Task<RepositoryResult<Guid>> CreateAsync(User user, CancellationToken cancellationToken = default);
    Task<RepositoryResult<User>> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}