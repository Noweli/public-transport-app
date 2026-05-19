namespace PublicTransport.Core.Domain.Models.Entities;

public sealed class Stop
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Street { get; set; }
    public required WGS84Coordinate Coordinate { get; set; }
}