using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Infrastructure.Persistence.Configurations;

public sealed class LineStopConfiguration : IEntityTypeConfiguration<LineStop>
{
    public void Configure(EntityTypeBuilder<LineStop> builder)
    {
        builder.HasKey(lineStop => lineStop.Id);

        builder.HasOne(lineStop => lineStop.Line)
            .WithMany(line => line.LineStops)
            .HasForeignKey(lineStop => lineStop.LineId);
        
        builder.HasOne(lineStop => lineStop.Stop)
            .WithMany(stop => stop.LineStops)
            .HasForeignKey(lineStop => lineStop.StopId);
    }
}