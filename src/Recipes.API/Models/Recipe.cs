using System.ComponentModel.DataAnnotations;

namespace cHub.Recipes.API.Models;

public class Recipe {
    public int Id { get; set; }

    [Required]
    public required string Title { get; set; }

    public Recipe() {}
}