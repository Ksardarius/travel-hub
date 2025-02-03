using cHub.Recipes.API.Infrastructure;
using cHub.Recipes.API.IntegrationEvents.EventHandling;
using cHub.Recipes.API.IntegrationEvents.Events;
using cHub.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using Rebus.Config;
using Rebus.Routing.TypeBased;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContextPool<RecipeContext>(opt =>
        {
            opt.UseNpgsql(builder.Configuration.GetConnectionString("RecipeContext"));
        });

        // builder.Services.AddDbContext<RecipeContext>(opt => {
        //     var bookStoreDatabaseSettings = builder.Configuration.GetSection("BookStoreDatabase").Get<BookStoreDatabaseSettings>()!;
        //     var mongoClient = new MongoClient(bookStoreDatabaseSettings.ConnectionString);
        //     opt.UseMongoDB(mongoClient, bookStoreDatabaseSettings.DatabaseName);
        // });

        builder.Services.AddMigration<RecipeContext, RecipeContextSeed>();

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