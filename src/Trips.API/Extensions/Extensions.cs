using cHub.Recipes.API.IntegrationEvents.EventHandling;
using Microsoft.EntityFrameworkCore;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Travel.Trips.API.Infrastructure;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<TripDbContext>(opt => opt
            .EnableSensitiveDataLogging()
            .UseMongoDB(builder.Configuration.GetConnectionString("TripsDb") ?? "", "tripsdb")
        );

        builder.Services.AddRebus(configure => configure
            .Routing(r =>
                r.TypeBased().MapAssemblyOf<Program>("recipes-queue")
            )
            .Options(o => o.EnableDiagnosticSources())
            .Transport(t =>
                t.UseRabbitMq(
                    builder.Configuration.GetConnectionString("RabbitMq"),
                    inputQueueName: "recipes-queue")
                    )// ,
                     // onCreated: async (bus) => {
                     //     await bus.Subscribe<CreateNewRecipe>();
                     // }
        );

        builder.Services.AddRebusHandler<CreateNewRecipeEventHandler>();
    }
}