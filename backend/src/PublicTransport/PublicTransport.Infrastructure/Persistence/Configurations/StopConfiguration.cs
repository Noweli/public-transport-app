using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Infrastructure.Persistence.Configurations;

public sealed class StopConfiguration : IEntityTypeConfiguration<Stop>
{
    public void Configure(EntityTypeBuilder<Stop> builder)
    {
        builder.HasKey(stop => stop.Id);
        
        builder.HasMany(line => line.LineStops)
            .WithOne(lineStop => lineStop.Stop);
    }
}