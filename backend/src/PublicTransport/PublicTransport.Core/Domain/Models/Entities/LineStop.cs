namespace PublicTransport.Core.Domain.Models.Entities;

public sealed class LineStop
{
    public int Id { get; set; }
    public required int LineId { get; set; }
    public required int StopId { get; set; }
    public int StopOrder { get; set; }
    public DateTimeOffset DepartureTime { get; set; }
}