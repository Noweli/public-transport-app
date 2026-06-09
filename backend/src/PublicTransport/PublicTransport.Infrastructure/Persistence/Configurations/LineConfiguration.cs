using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Infrastructure.Persistence.Configurations;

public sealed class LineConfiguration : IEntityTypeConfiguration<Line>
{
    public void Configure(EntityTypeBuilder<Line> builder)
    {
        builder.HasKey(line => line.Id);

        builder.HasMany(line => line.LineStops)
            .WithOne(lineStop => lineStop.Line);
    }
}