using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetJokesAPI.Models;


[BsonIgnoreExtraElements]
public class Joke
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("setup")]
    [JsonPropertyName("setup")]
    public string? Setup { get; set; }

    [BsonElement("punchline")]
    [JsonPropertyName("punchline")]
    public string? Punchline { get; set; }

}