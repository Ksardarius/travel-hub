using Microsoft.EntityFrameworkCore;
using Travel.Trips.API.Models;

namespace Travel.Trips.API.Infrastructure;

public partial class TravelDbContextSeed(ILogger<TripDbContext> logger) : IDbSeeder<TripDbContext>
{
    public async Task SeedAsync(TripDbContext context)
    {
        context.Database.OpenConnection();


        if (!context.Trips.Any())
        {
            var item = new Trip()
            {
                Id = 1,
                Destination = "LV"
            };

            await context.Trips.AddAsync(item);

            logger.LogInformation("Seeded trips with {NumItems} items", context.Trips.Count());
            await context.SaveChangesAsync();
        }
    }
}