namespace PublicTransport.Core.Domain.Models.Entities;

public sealed class Line
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}