namespace PublicTransport.Core.Domain.Models.Entities;

public sealed class Stop
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Street { get; set; }
    public required string Lat { get; set; }
    public required string Long { get; set; }
}