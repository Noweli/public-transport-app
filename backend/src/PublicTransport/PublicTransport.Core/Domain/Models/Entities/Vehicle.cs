namespace PublicTransport.Core.Domain.Models.Entities;

public sealed class Vehicle
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? AdditionalInformation { get; set; }
}