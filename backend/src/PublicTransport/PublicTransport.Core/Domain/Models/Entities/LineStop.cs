namespace PublicTransport.Core.Domain.Models.Entities;

public sealed class LineStop
{
    public required Guid Id { get; set; }
    public required int LineId { get; set; }
    public Line? Line { get; set; }
    
    public required int StopId { get; set; }
    public Stop? Stop { get; set; }
    
    public int StopOrder { get; set; }
    public DateTimeOffset DepartureTime { get; set; }
}