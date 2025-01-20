using BookStoreApi.Models;
using cHub.Recipes.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

public static class Extensions {
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContextPool<RecipeContext>(opt => {
            opt.UseNpgsql(builder.Configuration.GetConnectionString("RecipeContext"));
        });

        // builder.Services.AddDbContext<RecipeContext>(opt => {
        //     var bookStoreDatabaseSettings = builder.Configuration.GetSection("BookStoreDatabase").Get<BookStoreDatabaseSettings>()!;
        //     var mongoClient = new MongoClient(bookStoreDatabaseSettings.ConnectionString);
        //     opt.UseMongoDB(mongoClient, bookStoreDatabaseSettings.DatabaseName);
        // });

        builder.Services.AddMigration<RecipeContext, RecipeContextSeed>();
    }
}