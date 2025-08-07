using System.Diagnostics.CodeAnalysis;
using cHub.Recipes.API.IntegrationEvents.Events;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Rebus.Bus;
using Travel.Trips.API.Infrastructure;

namespace Travel.Trips.API.Grpc;

public class TripsService(
    TripDbContext dbContext,
    ILogger<TripsService> logger,
    IBus bus) : Trips.TripsBase
{
    public async override Task<GetAllTripsResponse> GetAllTrips(GetAllTripsRequest request, ServerCallContext context)
    {
        var pageSize = request.PageSize;
        var pageIndex = request.PageIndex;

        var root = dbContext.Trips.AsQueryable();

        var totalItems = await root
            .LongCountAsync();

        var itemsOnPage = await root
            .OrderBy(c => c.Destination)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        var response = new GetAllTripsResponse
        {
            Count = (int)totalItems,
            PageIndex = pageIndex,
            PageSize = pageSize,
        };

        foreach (var item in itemsOnPage)
        {
            response.Trips.Add(new Trip
            {
                Id = item.Id,
                Destination = item.Destination
            });
        }

        if (itemsOnPage.Count > 0)
        {
            await bus.Publish(new CreateNewRecipe(itemsOnPage[0].Id, itemsOnPage[0].Destination));
        }

        return response;
    }

    public override async Task<CreateTripResponse> CreateTrip(CreateTripRequest request, ServerCallContext context)
    {
        var trip = new Models.Trip
        {
            Id = request.Id,
            Destination = request.Destination
        };

        dbContext.Trips.Add(trip);

        await dbContext.SaveChangesAsync();

        return new CreateTripResponse
        {
            Status = 0
        };
    }

    public override async Task<AddTripVariantResponse> AddTripVariant(AddTripVariantRequest request, ServerCallContext context)
    {
        var trip = await dbContext.Trips.FirstOrDefaultAsync(f => f.Id == request.TripId);
        if (trip != null)
        {
            trip.Variants.Add(new Models.Trip.TripVariants
            {
                Name = request.Name
            });

            await dbContext.SaveChangesAsync();
        }

        return new AddTripVariantResponse
        {
            Status = 0
        };
    }

    [DoesNotReturn]
    private static void ThrowBasketDoesNotExist(string userId) => throw new RpcException(new Status(StatusCode.NotFound, $"Basket with buyer id {userId} does not exist"));
}