using cHub.Recipes.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace cHub.Recipes.API.Infrastructure.EntityConfigurations;

class RecipeEntityTypeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.ToTable("Recipe");

        builder.Property(r => r.Title)
            .HasMaxLength(100);

        builder.HasIndex(r => r.Title);
    }
}