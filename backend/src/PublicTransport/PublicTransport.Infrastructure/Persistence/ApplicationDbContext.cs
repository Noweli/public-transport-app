using Microsoft.EntityFrameworkCore;
using PublicTransport.Core.Domain.Models.Entities;

namespace PublicTransport.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Line> Lines => Set<Line>();
    public DbSet<LineStop> LineStops => Set<LineStop>();
    public DbSet<Stop> Stops => Set<Stop>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}