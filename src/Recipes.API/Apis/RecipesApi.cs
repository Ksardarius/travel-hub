
using cHub.Recipes.API.Infrastructure;
using cHub.Recipes.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cHub.Recipes.API;

public static class RecipesApi {
    public static IEndpointRouteBuilder MapRecipesApi(this IEndpointRouteBuilder app)
    {
        app.MapGet("/recipes", GetAllRecipes)
            .WithName("ListRecipes")
            .WithSummary("List recipes items")
            .WithDescription("Get a paginated list of recipes")
            .WithTags("Items");

        app.MapPost("/recipes", CreateItem)
            .WithName("CreateItem")
            .WithSummary("Create a recipe item")
            .WithDescription("Create a new recipe");

        return app;
    }

    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    public static async Task<Ok<PaginatedItems<Recipe>>> GetAllRecipes(
        [AsParameters] PaginationRequest paginationRequest,
        RecipeContext dbContext)
    {
        var pageSize = paginationRequest.PageSize;
        var pageIndex = paginationRequest.PageIndex;

        var root = dbContext.Recipes.AsQueryable();

        var totalItems = await root
            .LongCountAsync();

        var itemsOnPage = await root
            .OrderBy(c => c.Title)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return TypedResults.Ok(new PaginatedItems<Recipe>(pageIndex, pageSize, totalItems, itemsOnPage));
    }

    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    public static async Task<Created> CreateItem(
        RecipeContext dbContext,
        Recipe recipe)
    {
        var item = new Recipe
        {
            Id = recipe.Id,
            Title = recipe.Title
        };

        dbContext.Recipes.Add(item);
        await dbContext.SaveChangesAsync();

        return TypedResults.Created($"/recipes/{item.Id}");
    }
}