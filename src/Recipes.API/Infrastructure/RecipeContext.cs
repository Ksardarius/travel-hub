using cHub.Recipes.API.Infrastructure.EntityConfigurations;
using cHub.Recipes.API.Models;
using Microsoft.EntityFrameworkCore;

namespace cHub.Recipes.API.Infrastructure;

public class RecipeContext: DbContext {
    public RecipeContext(DbContextOptions<RecipeContext> options, IConfiguration configuration) : base(options) {
    }

    public DbSet<Recipe> Recipes {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RecipeEntityTypeConfiguration());
    }
}