using cHub.ServiceDefaults;
using Travel.Trips.API.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();
builder.AddApplicationServices();

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.MapGrpcService<TripsService>();

app.MapPrometheusScrapingEndpoint();

app.Run();
