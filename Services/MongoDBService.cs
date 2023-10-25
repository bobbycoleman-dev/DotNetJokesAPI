using DotNetJokesAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotNetJokesAPI.Services;

public class MongoDBService
{
    private readonly IMongoCollection<Joke> _jokeCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _jokeCollection = database.GetCollection<Joke>(mongoDBSettings.Value.CollectionName);
    }

    public async Task CreateAsync(Joke joke)
    {
        await _jokeCollection.InsertOneAsync(joke);
        return;
    }

    public async Task<List<Joke>> GetAsync()
    {
        return await _jokeCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateJokeAsync(string id, Joke updatedJoke)
    {
        FilterDefinition<Joke> filter = Builders<Joke>.Filter.Eq("Id", id);
        UpdateDefinition<Joke> update = Builders<Joke>.Update.Set("punchline", updatedJoke.Punchline).Set("setup", updatedJoke.Setup);
        // UpdateDefinition<Joke> update = Builders<Joke>.Update.AddToSet<string>("items", jokeId);
        await _jokeCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteAsync(string id)
    {
        FilterDefinition<Joke> filter = Builders<Joke>.Filter.Eq("Id", id);
        await _jokeCollection.DeleteOneAsync(filter);
        return;
    }
}
