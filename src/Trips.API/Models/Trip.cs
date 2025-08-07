using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Travel.Trips.API.Models;

public class Trip
{
    public class TripVariants
    {
        [BsonElement("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    [BsonId]
    public ObjectId _id { get; set; }

    [BsonElement("id")]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [BsonElement("destination")]
    [JsonPropertyName("destination")]
    public required string Destination { get; set; }

    [BsonElement("variants")]
    [JsonPropertyName("variants")]
    public List<TripVariants> Variants { get; set; } = [];
}