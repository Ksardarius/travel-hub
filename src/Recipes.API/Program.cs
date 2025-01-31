using BookStoreApi.Models;
using BookStoreApi.Services;
using cHub.Recipes.API;
using cHub.Recipes.API.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddApplicationServices();
        
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
// builder.Services.AddProblemDetails();


builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddSingleton<BooksService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
    // app.MapOpenApi();
}

app.MapGrpcService<RecipesService>();
// app.UseStatusCodePages();
//app.MapRecipesApi();

app.Run();
