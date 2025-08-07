using Microsoft.EntityFrameworkCore;
using Travel.Trips.API.Infrastructure.EntityConfigurations;
using Travel.Trips.API.Models;

namespace Travel.Trips.API.Infrastructure;

public class TripDbContext : DbContext
{
    public TripDbContext(DbContextOptions<TripDbContext> options) : base(options)
    {
    }

    public DbSet<Trip> Trips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TripEntityTypeConfiguration());
    }
}