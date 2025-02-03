using System.Diagnostics.CodeAnalysis;
using System.Linq;
using cHub.Recipes.API.Infrastructure;
using cHub.Recipes.API.IntegrationEvents.Events;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Rebus.Bus;

namespace cHub.Recipes.API.Grpc;

public class RecipesService(
    RecipeContext dbContext,
    ILogger<RecipesService> logger,
    IBus bus) : Recipes.RecipesBase
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

        await bus.Publish(new CreateNewRecipe(itemsOnPage[0].Id, itemsOnPage[0].Title));

        return response;
    }

    [DoesNotReturn]
    private static void ThrowBasketDoesNotExist(string userId) => throw new RpcException(new Status(StatusCode.NotFound, $"Basket with buyer id {userId} does not exist"));
}