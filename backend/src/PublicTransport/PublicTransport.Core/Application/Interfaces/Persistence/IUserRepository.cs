using PublicTransport.Core.Domain.Models;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Core.Application.Interfaces.Persistence;

public interface IUserRepository
{
    Task<Result<User>> CreateAsync(User user, CancellationToken cancellationToken = default);
    Task<Result<User>> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}