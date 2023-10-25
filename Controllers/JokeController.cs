using System;
using DotNetJokesAPI.Models;
using DotNetJokesAPI.Services;
using Microsoft.AspNetCore.Mvc;


namespace DotNetJokesAPI.Controllers;

[Controller]
[Route("api/jokes")]
public class JokeController : Controller
{
    private readonly MongoDBService _mongoDBService;

    public JokeController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Joke>> Get()
    {
        return await _mongoDBService.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Joke joke)
    {
        await _mongoDBService.CreateAsync(joke);
        return CreatedAtAction(nameof(Get), new { id = joke.Id }, joke);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJoke(string id, [FromBody] Joke updatedJoke)
    {
        await _mongoDBService.UpdateJokeAsync(id, updatedJoke);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }

}