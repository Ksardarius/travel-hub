using System.Diagnostics.CodeAnalysis;
using System.Linq;
using cHub.Recipes.API.Infrastructure;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace cHub.Recipes.API.Grpc;

public class RecipesService(
    RecipeContext dbContext,
    ILogger<RecipesService> logger) : Recipes.RecipesBase
{
    public async override Task<GetAllRecipesResponse> GetAllRecipes(GetAllRecipesRequest request, ServerCallContext context)
    {
        var pageSize = request.PageSize;
        var pageIndex = request.PageIndex;

        var root = dbContext.Recipes.AsQueryable();

        var totalItems = await root
            .LongCountAsync();

        var itemsOnPage = await root
            .OrderBy(c => c.Title)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        var response = new GetAllRecipesResponse {
            Count = ((int)totalItems),
            PageIndex = pageIndex,
            PageSize = pageSize,
        };

        foreach (var item in itemsOnPage) {
            response.Recipes.Add(new Recipe {
                Id = item.Id,
                Title = item.Title
            });
        }

        return response;
    }

    [DoesNotReturn]
    private static void ThrowBasketDoesNotExist(string userId) => throw new RpcException(new Status(StatusCode.NotFound, $"Basket with buyer id {userId} does not exist"));
}