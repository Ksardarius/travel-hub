using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Travel.Trips.API.Models;

namespace Travel.Trips.API.Infrastructure.EntityConfigurations;

class TripEntityTypeConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToCollection("trips");
    }
}