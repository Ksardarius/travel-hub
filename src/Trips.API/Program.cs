using cHub.ServiceDefaults;
using Travel.Trips.API.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();
builder.AddApplicationServices();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
// builder.Services.AddProblemDetails();


builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
    // app.MapOpenApi();
}

app.MapGrpcService<TripsService>();

app.MapPrometheusScrapingEndpoint();
// app.UseStatusCodePages();
//app.MapRecipesApi();

app.Run();
