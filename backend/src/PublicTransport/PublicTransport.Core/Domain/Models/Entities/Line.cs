namespace PublicTransport.Core.Domain.Models.Entities;

public sealed class Line
{
    public int Id { get; set; }
    public required string Name { get; set; }
}