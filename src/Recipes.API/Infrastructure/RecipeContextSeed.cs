using cHub.Recipes.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;

namespace cHub.Recipes.API.Infrastructure;

public partial class RecipeContextSeed(ILogger<RecipeContextSeed> logger) : IDbSeeder<RecipeContext>
{
    public async Task SeedAsync(RecipeContext context)
    {
        context.Database.OpenConnection();
        ((NpgsqlConnection)context.Database.GetDbConnection()).ReloadTypes();

        if (!context.Recipes.Any())
        {
            var item = new Recipe() {
                Id = 1,
                Title = "MyTitle"
            };

            await context.Recipes.AddAsync(item);

            logger.LogInformation("Seeded recipes with {NumItems} items", context.Recipes.Count());
            await context.SaveChangesAsync();
        }
    }
}