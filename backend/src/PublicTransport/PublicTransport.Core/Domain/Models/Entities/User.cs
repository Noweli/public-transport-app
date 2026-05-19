using PublicTransport.Core.Domain.Constants;

namespace PublicTransport.Core.Domain.Models.Entities;

public sealed class User
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; } = UserRole.User;
}