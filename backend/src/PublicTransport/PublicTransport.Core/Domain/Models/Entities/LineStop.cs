namespace PublicTransport.Core.Domain.Models.Entities;

public sealed class LineStop
{
    public required Guid Id { get; set; }
    public required int LineId { get; set; }
    public required int StopId { get; set; }
    public int StopOrder { get; set; }
    public DateTimeOffset DepartureTime { get; set; }
}